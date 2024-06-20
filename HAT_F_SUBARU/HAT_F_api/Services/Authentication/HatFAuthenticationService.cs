using HAT_F_api.CustomModels;
using HAT_F_api.Models;
using HAT_F_api.Utils;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using NuGet.Common;
using System.Diagnostics.Eventing.Reader;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HAT_F_api.Services.Authentication
{
    /// <summary>
    /// HAT-F認証サービス
    /// </summary>
    public class HatFAuthenticationService
    {
        private static readonly HMACSHAAlgorithm _jwtAlgorithm = new HMACSHA256Algorithm();
        private readonly HatFContext _hatFContext;
        private readonly IConfiguration _configuration;
        private static NLog.ILogger _logger;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly HatFLoginResultAccesser _hatFLoginResultAccesser;

        public HatFAuthenticationService(IHttpContextAccessor httpContextAccessor, HatFContext hatFContext, IConfiguration configuration, NLog.ILogger logger, HatFLoginResultAccesser hatFLoginResultAccesser)
        {
            _httpContextAccessor = httpContextAccessor;
            _hatFContext = hatFContext;
            _configuration = configuration;
            _logger = logger;
            _hatFLoginResultAccesser = hatFLoginResultAccesser;
        }

        private JwtBuilder CreateJwtBuilder()
        {
            string encryptionSecretKey = _configuration["HatFAuthentication:Jwt:EncryptionSecretKey"];  // 暗号化キー

            var builder = new JwtBuilder()
                .WithAlgorithm(_jwtAlgorithm)       // 暗号方式
                .WithSecret(encryptionSecretKey);   // 暗号化キー

            return builder;
        }

        /// <summary>
        /// JWTトークン文字列生成
        /// </summary>
        public string GenerateJwtToken(HatFLoginResult loginResult)
        {
            //https://zenn.dev/nemuvski/scraps/0dc0217515e4e5
            int periodMinutes = int.Parse(_configuration["HatFAuthentication:Jwt:PeriodMinutes"]);  // JWT有効期間(分)
            DateTimeOffset validityPeriod = DateTimeOffset.UtcNow.AddMinutes(periodMinutes);

            JwtBuilder builder = CreateJwtBuilder()
                .AddClaim("exp", validityPeriod.ToUnixTimeSeconds());   // JWT有効期間(分)

            foreach (PropertyInfo prop in loginResult.GetType().GetProperties())
            {
                object val = prop.GetValue(loginResult);
                builder = builder.AddClaim(prop.Name, val);
            }

            string jwtToken = builder.Encode();
            return jwtToken;
        }

        /// <summary>
        /// JWTトークン文字列をHatFLoginResultに展開
        /// </summary>
        public HatFLoginResult ExpandJwtToken(string jwtToken)
        {
            HatFLoginResult loginResult = new HatFLoginResult();

            try
            {
                // JWTをJSONに展開
                JwtBuilder builder = CreateJwtBuilder().MustVerifySignature();
                string json = builder.Decode(jwtToken);
                System.Diagnostics.Debug.WriteLine(json);

                // JSON からオブジェクトへのデシリアライズ
                HatFLoginResult jwtContent = JsonSerializer.Deserialize<HatFLoginResult>(json);

#if DEBUG
                foreach (PropertyInfo prop in jwtContent.GetType().GetProperties())
                {
                    object val = prop.GetValue(jwtContent);
                    System.Diagnostics.Debug.WriteLine($"{prop.Name}: {val}");
                }
#endif
                _httpContextAccessor.HttpContext.Items["HatFLoginResult"] = jwtContent;

                return jwtContent;
            }
            catch (TokenExpiredException tee)
            {
                string logMessage = "JWT を展開しようとしましたが、有効期限に達していました";
                _logger.Info(tee, logMessage);

                loginResult.ErrorMessage = "ログイン セッションの有効期限に達しました。再度ログインしてください。";

                // ログインしていないユーザーをセット
                _httpContextAccessor.HttpContext.Items["HatFLoginResult"] = CreateFailedLoginedResult();
            }
            catch (SignatureVerificationException sve)
            {
                string logMessage = "JWT を展開しようとしましたが、破損しているか署名が不正でした";
                _logger.Warn(sve, logMessage);

                loginResult.ErrorMessage = "ログイン情報が破損していました。再度ログインしてください。";

                // ログインしていないユーザーをセット
                _httpContextAccessor.HttpContext.Items["HatFLoginResult"] = CreateFailedLoginedResult();
            }

            // 検証エラー
            loginResult.LoginSucceeded = false;
            return loginResult;
        }

        private HatFLoginResult CreateFailedLoginedResult()
        {
            HatFLoginResult loginResult = new HatFLoginResult();
            loginResult.LoginSucceeded = false;
            loginResult.EmployeeName = "未ログイン ユーザー";
            return loginResult;
        }

        /// <summary>
        /// ログイン認証
        /// </summary>
        /// <remarks>
        /// 戻り値のHatFLoginRequest.LoginSuccessedがログイン成否を表します
        /// </remarks>
        public async Task<HatFLoginResult> AuthenticationAsync(HatFLoginRequest request)
        {
            string empCode = request.EmployeeCode;
            string hashedPassword = HashPassword(request.LoginPassword);

            var query = _hatFContext.Employees.Join(
                    _hatFContext.UserAuthentications,
                    emp => emp.EmpId,
                    auth => auth.EmpId,
                    (emp, auth) => new { emp, auth }
                )
                .Where(item =>
                    item.emp.EmpCode == empCode
#if DEBUG
                    && (item.auth.LoginPassword == hashedPassword || item.auth.LoginPassword == request.LoginPassword)
#else
                    && item.auth.LoginPassword == hashedPassword
#endif
                )
                .Select(item => item.emp);

            HatFLoginResult loginResult = new HatFLoginResult();

            Employee employee = await query.SingleOrDefaultAsync();
            if (employee == null)
            {
                string logMessage = $"ログイン 認証エラー 社員番号: {request.EmployeeCode}";
                _logger.Info(logMessage);

                loginResult.LoginSucceeded = false;
                loginResult.ErrorMessage = "社員番号またはパスワードが正しくありません。";
            }
            else
            {
                string logMessage = $"ログイン 認証成功 社員番号: {request.EmployeeCode}";
                _logger.Info(logMessage);

                loginResult.LoginSucceeded = true;
                loginResult.ErrorMessage = "";
                loginResult.EmployeeId = employee.EmpId;
                loginResult.EmployeeCode = employee.EmpCode ?? "";
                loginResult.EmployeeName = employee.EmpName ?? "";
                loginResult.EmployeeNameKana = employee.EmpKana ?? "";
                loginResult.EmployeeTag = employee.EmpTag ?? "";

                var deptQuery = _hatFContext.DeptMsts.Where(x=>x.DeptCode == employee.DeptCode);
                var dept = deptQuery.SingleOrDefault();
                loginResult.TeamCode = dept?.TeamCd ?? "";

                List<HatFUserRole> roles = new List<HatFUserRole>();

                var roleQuery = _hatFContext.UserAssignedRoles.Where(item => item.EmpId == employee.EmpId);
                await foreach (var roleItem in roleQuery.AsAsyncEnumerable())
                {
                    if (Enum.TryParse(roleItem.UserRoleCd, out HatFUserRole roleEnum))
                    {
                        roles.Add(roleEnum);
                    }
                    else
                    {
                        logMessage = $"{nameof(HatFUserRole)}列挙型で定義されていないロールが{nameof(UserAssignedRole)}テーブルにに見つかりました: {roleItem.UserRoleCd}";
                        _logger.Warn(logMessage);
                    }
                }

                // enum名称をカンマ区切りで連結して格納
                loginResult.Roles = string.Join(",", roles);
            }

            if (employee != null)
            {
                loginResult.JwtToken = GenerateJwtToken(loginResult);
            }

            return loginResult;
        }

        /// <summary>
        /// パスワードをハッシュ化します
        /// </summary>
        public string HashPassword(string rawPassword)
        {
            string salt = _configuration["HatFAuthentication:PasswordHashSalt"];

            string hashedPassword;
            using (var sha = SHA256.Create())
            {
                byte[] sourceByte = Encoding.UTF8.GetBytes(rawPassword + salt);
                byte[] hashedBytes = sha.ComputeHash(sourceByte);
                hashedPassword = Convert.ToBase64String(hashedBytes);
            }

            return hashedPassword;
        }


        public static HatFLoginResult HatFLoginResultFromHttpContext(HttpContext context)
        {
            var login = new HatFLoginResult();

            var roles = new StringBuilder();
            if (context?.User?.Claims != null)
            {
                foreach (var claim in context.User.Claims)
                {
                    switch (claim.Type)
                    {
                        case nameof(HatFLoginResult.EmployeeId):
                            login.EmployeeId = int.Parse(claim.Value);
                            break;
                        case nameof(HatFLoginResult.EmployeeCode):
                            login.EmployeeCode = claim.Value;
                            break;
                        case nameof(HatFLoginResult.EmployeeName):
                            login.EmployeeName = claim.Value;
                            break;
                        case nameof(HatFLoginResult.EmployeeNameKana):
                            login.EmployeeNameKana = claim.Value;
                            break;
                        case nameof(HatFLoginResult.EmployeeTag):
                            login.EmployeeTag = claim.Value;
                            break;
                        case nameof(HatFLoginResult.Roles):
                            login.EmployeeTag = claim.Value;
                            break;
                    }
                }
            }

            return login;
        }


        /// <summary>
        /// パスワード変更
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HatFApiServiceException"></exception>
        public async Task<int> PutLoginPassword(int empId, string newPassword, string currentPassword = null, bool admin = false)
        {
            // TODO: パスワードの複雑さの要件はあるか？
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException(nameof(currentPassword));
            }

            UserAuthentication userAuthentication;

            if (admin)
            {
                // APIエンドポイントのアクセス権制御に倒した
                //string roleString = _hatFLoginResultAccesser.HatFLoginResult.Roles;
                //bool isInRole = HatFUserRoleHelper.ContainsRole(roleString, HatFUserRole.MasterEdit);   //TODO:必要権限みなおし

                //if (!isInRole)
                //{
                //    throw new HatFApiServiceException("他ユーザーのパスワード変更権限はありません。");
                //}

                var query = _hatFContext.UserAuthentications
                    .Where(x => x.EmpId == empId);
                userAuthentication = await query.SingleAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(currentPassword))
                {
                    throw new ArgumentNullException(nameof(currentPassword));
                }

                string hashedCurrentPassword = HashPassword(currentPassword);

                var query = _hatFContext.UserAuthentications
                    .Where(x => x.EmpId == empId);
                
                userAuthentication = await query.SingleAsync();
#if DEBUG
                if (userAuthentication.LoginPassword != hashedCurrentPassword && userAuthentication.LoginPassword != currentPassword)
#else
                if (userAuthentication.LoginPassword != hashedCurrentPassword)
#endif
                {
                    throw new HatFApiServiceException("現在のパスワードが一致しないため変更できません。", "パスワードが正しくありません。");
                }
            }

            // パスワードを変更
            string newHashedPassword = HashPassword(newPassword);
            userAuthentication.LoginPassword = newHashedPassword;

            int count = await _hatFContext.SaveChangesAsync();
            return count;
        }
    }
}
