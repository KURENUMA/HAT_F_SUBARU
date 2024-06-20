using HAT_F_api.CustomModels;
using Humanizer;
using Microsoft.AspNetCore.Authentication;  
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace HAT_F_api.Services.Authentication
{
    /// <summary>
    /// HTTPリクエストがログイン済ユーザーか判定する認証処理
    /// </summary>
    public class HatFAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IOptionsMonitor<AuthenticationSchemeOptions> _options;
        private ILoggerFactory _logger;
        private UrlEncoder _encoder;
        private HatFAuthenticationService _hatFAuthenticationService;

        public HatFAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            HatFAuthenticationService hatFAuthenticationService
            ) : base(options, logger, encoder)
        {
            _options = options;
            _logger = logger;
            _encoder = encoder;
            _hatFAuthenticationService = hatFAuthenticationService;
        }

        /// <summary>
        /// HTTPリクエストごとの認証処理
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// ここでクライアントのアクセスを判断してアクセス許可を処理します
        /// </remarks>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // HTTPヘッダーのAuthorizationヘッダーを確認
            string authorizationHeader = Context.Request.Headers.Authorization;

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                // Authorizationヘッダーが空なら不許可
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            // Authorizationヘッダーに「Bearer ********」形式の文字列(*******はJWT)がある想定
            string bearer = authorizationHeader.Substring("Bearer ".Length).Trim();

            // JWTを展開
            var result = _hatFAuthenticationService.ExpandJwtToken(bearer);

            if (!result.LoginSucceeded)
            {
                // JWTを正常に展開できなかった場合、認証失敗を返す
                return Task.FromResult(AuthenticateResult.Fail(result.ErrorMessage));
            }
            else
            {
                var claims = new List<Claim>();

                // ユーザー名
                claims.Add(new Claim(ClaimTypes.Name, result.EmployeeName));

                // HttpContextから取得したいかもしれない情報を保持しておく
                claims.Add(new Claim(nameof(HatFLoginResult.EmployeeId), result.EmployeeId.ToString()));
                claims.Add(new Claim(nameof(HatFLoginResult.EmployeeCode), result.EmployeeCode));
                claims.Add(new Claim(nameof(HatFLoginResult.EmployeeName), result.EmployeeName));
                claims.Add(new Claim(nameof(HatFLoginResult.EmployeeNameKana), result.EmployeeNameKana));
                claims.Add(new Claim(nameof(HatFLoginResult.EmployeeTag), result.EmployeeTag));
                claims.Add(new Claim(nameof(HatFLoginResult.TeamCode), result.TeamCode));
                claims.Add(new Claim(nameof(HatFLoginResult.Roles), result.Roles));

                string[] roles = result.Roles.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (string role in roles)
                {
                    // ここでASP.NET Coreがクライアントユーザーが保持するロールを認識する情報をセット
                    // コントローラーに設定する [Authorize(Roles = "ロール名")] に一致します
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));

                return Task.FromResult(AuthenticateResult.Success(
                    // 認証OKを返す
                    new AuthenticationTicket(claimsPrincipal, "HatFApi")
                ));
            }
        }
    }
}
