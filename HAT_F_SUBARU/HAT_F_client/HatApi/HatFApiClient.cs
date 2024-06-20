using System.Net.Http;
using System.Threading.Tasks;
using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Constants;
using HatFClient.HatApi.Models;
using HatFClient.Models;
using RestSharp;

namespace HatFClient.HatApi
{
    /// <summary>橋本総業一貫化APIクライアント</summary>
    public class HatFApiClient : ApiClient
    {
        /// <summary>アクセストークン</summary>
        public string AccessToken { get; set; }

        /// <summary>コンストラクタ</summary>
        /// <param name="baseUrl">ベースURL</param>
        public HatFApiClient(string baseUrl)
            : base(baseUrl)
        {
        }

        #region ログイン/ログアウト

        /// <summary>ログインAPIの実行</summary>
        /// <param name="userId">ユーザID</param>
        /// <param name="password">パスワード</param>
        /// <returns>タスク</returns>
        public async Task<ApiResponse<HatFLoginResult>> LoginAsync(string userId, string password)
        {
            var result = await PostAsync<HatFLoginResult>(ApiResources.HatF.Login.Auth, new HatFLoginRequest()
            {
                EmployeeCode = userId,
                LoginPassword = password,
            });

            if (ApiHelper.IsPositiveResponse(result))
            {
                HatFLoginResult hatFLoginResult = result.Data;
                if (hatFLoginResult.LoginSucceeded)
                {
                    AccessToken = result.Data.JwtToken;
                }
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