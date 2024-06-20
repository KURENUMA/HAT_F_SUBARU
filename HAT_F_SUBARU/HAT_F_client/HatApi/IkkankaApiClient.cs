using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.HatApi.Models.Hat;
using RestSharp;

namespace HatFClient.HatApi
{
    /// <summary>橋本総業一貫化APIクライアント</summary>
    public class IkkankaApiClient : ApiClient
    {
        /// <summary>アクセストークン</summary>
        public string AccessToken { get; set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="baseUrl">ベースURL</param>
        public IkkankaApiClient(string baseUrl)
            : base(baseUrl)
        {
        }

        #region ログイン/ログアウト

        /// <summary>ログインAPIの実行</summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <returns>タスク</returns>
        public async Task<ApiResponse<LoginResult>> LoginAsync(string userId, string password)
        {
            var hostName = Dns.GetHostName();
            var ipAddress = Dns.GetHostAddresses(hostName).Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            var result = await PostAsync<LoginResult>(ApiResources.Ikkanka.Login, new
            {
                UserId = userId,
                Password = password,
                HostName = hostName,
                IpAddr = ipAddress.ToString(),
                AppId = "HAT-F",
                // 橋本総業側のバージョンチェックを回避するために最大限大きな値を指定する
                AppVer = "999.999.999",
            });

            if (!string.IsNullOrEmpty(result.Errors.StatusMessage) || !string.IsNullOrEmpty(result.Errors.StatusCode))
            {
                AccessToken = result.Data.UserInfo.AccessToken;
            }

            return result;
        }

        /// <summary>ログアウト</summary>
        public void Logout()
        {
            // アクセストークンをクリアするだけ
            // LoginAsyncの対として定義しただけなのでこのメソッドは無くても問題ない
            AccessToken = null;
        }

        #endregion ログイン/ログアウト

        #region 仮想関数

        /// <summary>リクエストヘッダを設定する</summary>
        /// <param name="method">Httpメソッド</param>
        /// <param name="resource">リクエスト先</param>
        /// <param name="request">リクエスト情報</param>
        protected override void AddRequestHeader(HttpMethod method, string resource, RestRequest request)
        {
            base.AddRequestHeader(method, resource, request);

            // トークン設定
            if (!string.IsNullOrEmpty(AccessToken))
            {
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
            }
        }

        #endregion 仮想関数
    }
}