using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HAT_F_api.CustomModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Polly;
using RestSharp;
using System.IO;

namespace HatFClient.HatApi
{
    /// <summary><see cref="RestClient"/>を拡張し、Get/Post/Put/Deleteを使いやすくしたクラス</summary>
    public class ApiClient : RestClient
    {
        private const int ServiceUnavailableRetryCount = 3;

        /// <summary>
        /// RestSharpのThrowOnAnyError = falseオプションが機能していないためエラーメッセージで判定
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private bool IsForbidden(Exception exception)
        {
            bool isForbidden = (exception.Message ?? "").ToLower().Contains("forbidden");
            return isForbidden;
        }

        /// <summary>デバッグ用に応答内容をダンプする</summary>
        public bool DumpResponse { get; set; }

        /// <summary>プロパティ名の変更ルールをCamelCaseにするための設定オブジェクト</summary>
        private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
        };

        #region RestClientのコンストラクタをすべてラッピング

        /// <summary>
        /// Creates an instance of RestClient using the provided <see cref="RestClientOptions"/>
        /// </summary>
        /// <param name="options">Client options</param>
        /// <param name="configureDefaultHeaders">Delegate to add default headers to the wrapped HttpClient instance</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        /// <param name="useClientFactory">Set to true if you wish to reuse the <seealso cref="HttpClient"/> instance</param>
        public ApiClient(
            RestClientOptions options,
            ConfigureHeaders configureDefaultHeaders = null,
            ConfigureSerialization configureSerialization = null,
            bool useClientFactory = false)
            : base(options, configureDefaultHeaders, configureSerialization, useClientFactory)
        {
        }

        /// <summary>
        /// Creates an instance of RestClient using the default <see cref="RestClientOptions"/>
        /// </summary>
        /// <param name="configureRestClient">Delegate to configure the client options</param>
        /// <param name="configureDefaultHeaders">Delegate to add default headers to the wrapped HttpClient instance</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        /// <param name="useClientFactory">Set to true if you wish to reuse the <seealso cref="HttpClient"/> instance</param>
        public ApiClient(
            ConfigureRestClient configureRestClient = null,
            ConfigureHeaders configureDefaultHeaders = null,
            ConfigureSerialization configureSerialization = null,
            bool useClientFactory = false)
            : base(configureRestClient, configureDefaultHeaders, configureSerialization, useClientFactory)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates an instance of RestClient using a specific BaseUrl for requests made by this client instance
        /// </summary>
        /// <param name="baseUrl">Base URI for the new client</param>
        /// <param name="configureRestClient">Delegate to configure the client options</param>
        /// <param name="configureDefaultHeaders">Delegate to add default headers to the wrapped HttpClient instance</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        /// <param name="useClientFactory">Set to true if you wish to reuse the <seealso cref="HttpClient"/> instance</param>
        public ApiClient(
            Uri baseUrl,
            ConfigureRestClient configureRestClient = null,
            ConfigureHeaders configureDefaultHeaders = null,
            ConfigureSerialization configureSerialization = null,
            bool useClientFactory = false)
            : base(baseUrl, configureRestClient, configureDefaultHeaders, configureSerialization, useClientFactory)
        {
        }

        /// <summary>
        /// Creates an instance of RestClient using a specific BaseUrl for requests made by this client instance
        /// </summary>
        /// <param name="baseUrl">Base URI for this new client as a string</param>
        /// <param name="configureRestClient">Delegate to configure the client options</param>
        /// <param name="configureDefaultHeaders">Delegate to add default headers to the wrapped HttpClient instance</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        public ApiClient(
            string baseUrl,
            ConfigureRestClient configureRestClient = null,
            ConfigureHeaders configureDefaultHeaders = null,
            ConfigureSerialization configureSerialization = null)
            : base(baseUrl, configureRestClient, configureDefaultHeaders, configureSerialization)
        {
        }

        /// <summary>
        /// Creates an instance of RestClient using a shared HttpClient and specific RestClientOptions and does not allocate one internally.
        /// </summary>
        /// <param name="httpClient">HttpClient to use</param>
        /// <param name="options">RestClient options to use</param>
        /// <param name="disposeHttpClient">True to dispose of the client, false to assume the caller does (defaults to false)</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        public ApiClient(
            HttpClient httpClient,
            RestClientOptions options,
            bool disposeHttpClient = false,
            ConfigureSerialization configureSerialization = null)
            : base(httpClient, options, disposeHttpClient, configureSerialization)
        {
        }

        /// <summary>
        /// Creates an instance of RestClient using a shared HttpClient and does not allocate one internally.
        /// </summary>
        /// <param name="httpClient">HttpClient to use</param>
        /// <param name="disposeHttpClient">True to dispose of the client, false to assume the caller does (defaults to false)</param>
        /// <param name="configureRestClient">Delegate to configure the client options</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        public ApiClient(
            HttpClient httpClient,
            bool disposeHttpClient = false,
            ConfigureRestClient configureRestClient = null,
            ConfigureSerialization configureSerialization = null)
            : base(httpClient, disposeHttpClient, configureRestClient, configureSerialization)
        {
        }

        /// <summary>
        /// Creates a new instance of RestSharp using the message handler provided. By default, HttpClient disposes the provided handler
        /// when the client itself is disposed. If you want to keep the handler not disposed, set disposeHandler argument to false.
        /// </summary>
        /// <param name="handler">Message handler instance to use for HttpClient</param>
        /// <param name="disposeHandler">Dispose the handler when disposing RestClient, true by default</param>
        /// <param name="configureRestClient">Delegate to configure the client options</param>
        /// <param name="configureSerialization">Delegate to configure serialization</param>
        public ApiClient(
            HttpMessageHandler handler,
            bool disposeHandler = true,
            ConfigureRestClient configureRestClient = null,
            ConfigureSerialization configureSerialization = null)
            : base(handler, disposeHandler, configureRestClient, configureSerialization)
        {
        }

        #endregion RestClientのコンストラクタをすべてラッピング

        #region 仮想関数

        /// <summary>リクエストヘッダを設定する</summary>
        /// <param name="method">Httpメソッド</param>
        /// <param name="resource">リクエスト先</param>
        /// <param name="request">リクエスト情報</param>
        protected virtual void AddRequestHeader(HttpMethod method, string resource, RestRequest request)
        {
            if (method != HttpMethod.Get)
            {
                request.AddHeader("Content-Type", "application/json");
            }
        }

        #endregion 仮想関数

        #region リクエスト送信（非同期）

        /// <summary>POST/PUT/DELETEなどのリクエストボディがあるリクエストを送信する</summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="method">Httpメソッド</param>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        protected async Task<RestResponse> ExecuteAsync(HttpMethod method, string resource, IEnumerable<KeyValuePair<string, object>> queryParameters, object body)
        {
            var request = SetQueryParameter(new RestRequest(resource), queryParameters);

            // リクエストボディの設定
            request.AddBody(body ?? new { });
            // リクエストヘッダの設定
            AddRequestHeader(method, resource, request);

            // API実行
            Func<RestClient, RestRequest, CancellationToken, Task<RestResponse>> executeCore =
                method == HttpMethod.Post ? RestClientExtensions.PostAsync :
                method == HttpMethod.Put ? RestClientExtensions.PutAsync :
                method == HttpMethod.Delete ? RestClientExtensions.DeleteAsync : null;

            // ServiceUnavailableRetryCount 回までリトライする
            // リトライしても失敗した場合には例外が再スローされる
            RestResponse restResponse = await Policy.HandleResult<RestResponse>(x => IsRetryCondition(x))
                .RetryAsync(ServiceUnavailableRetryCount)
                .ExecuteAsync(async () =>
                {
                    return await executeCore(this, request, default);
                });

            if (DumpResponse)
            {
                // デバッグコンソールに応答内容を出力
                Debug.WriteLine(restResponse.Content);
            }
            return restResponse;
        }

        private bool IsRetryCondition(RestResponse restResponse)
        {
            // ステータスコードが 503 の場合はリトライ対象
            if (restResponse.StatusCode == HttpStatusCode.ServiceUnavailable) return true;

            return false;
        }

        /// <summary>POST/PUT/DELETEなどのリクエストボディがあるリクエストを送信する</summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="method">Httpメソッド</param>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        protected async Task<ApiResponse<T>> ExecuteAsync<T>(HttpMethod method, string resource, IEnumerable<KeyValuePair<string, object>> queryParameters, object body)
        {
            //https://learn.microsoft.com/ja-jp/dotnet/fundamentals/networking/http/httpclient#http-error-handling

            try
            {
                RestResponse restResponse = await ExecuteAsync(method, resource, queryParameters, body);

                if (!restResponse.IsSuccessful)
                {
                    // ネットワーク でエラーが起きた場合の対応
                    var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                    apiErrorResponse.Message = "サーバー呼出でエラーが発生しました。";
                    apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = restResponse.ErrorMessage, ValidationErrors = null };
                    return apiErrorResponse;
                }

                return JsonConvert.DeserializeObject<ApiResponse<T>>(restResponse.Content, _serializerSettings);
            }
            catch (Exception e) when (e is HttpException || e is HttpRequestException || e is OperationCanceledException)
            {
                // クライアントでエラーが起きた場合の対応
                var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                apiErrorResponse.Message = IsForbidden(e) ? "権限が不足しています。" : "ネットワークでエラーが発生しました。";
                apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = apiErrorResponse.Message, ValidationErrors = null };
                return apiErrorResponse;
            }
        }

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> PostAsync<T>(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => ExecuteAsync<T>(HttpMethod.Post, resource, queryParameters, body);

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> PostAsync<T>(string resource, object body)
            => ExecuteAsync<T>(HttpMethod.Post, resource, null, body);

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>非同期タスク（戻り値なし）</returns>
        public Task PostAsync(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => ExecuteAsync(HttpMethod.Post, resource, queryParameters, body);

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>非同期タスク（戻り値なし）</returns>
        public Task PostAsync(string resource, object body)
            => ExecuteAsync(HttpMethod.Post, resource, null, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> PutAsync<T>(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => ExecuteAsync<T>(HttpMethod.Put, resource, queryParameters, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> PutAsync<T>(string resource, object body)
            => ExecuteAsync<T>(HttpMethod.Put, resource, null, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>非同期タスク（戻り値なし）</returns>
        public Task PutAsync(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => ExecuteAsync(HttpMethod.Put, resource, queryParameters, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>非同期タスク（戻り値なし）</returns>
        public Task PutAsync(string resource, object body)
            => ExecuteAsync(HttpMethod.Put, resource, null, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> DeleteAsync<T>(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => ExecuteAsync<T>(HttpMethod.Delete, resource, queryParameters, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> DeleteAsync<T>(string resource, object body)
            => ExecuteAsync<T>(HttpMethod.Delete, resource, null, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>非同期タスク（戻り値なし）</returns>
        public Task DeleteAsync(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => ExecuteAsync(HttpMethod.Delete, resource, queryParameters, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>非同期タスク（戻り値なし）</returns>
        public Task DeleteAsync(string resource, object body)
            => ExecuteAsync(HttpMethod.Delete, resource, null, body);

        /// <summary>Getリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="parameters">パラメータ</param>
        /// <remarks><paramref name="parameters"/>は<see cref="Dictionary{TKey, TValue}"/>を推奨</remarks>
        /// <returns>レスポンス</returns>
        public async Task<ApiResponse<T>> GetAsync<T>(string resource, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var request = SetQueryParameter(new RestRequest(resource), parameters);

            // リクエストヘッダの設定
            AddRequestHeader(HttpMethod.Get, resource, request);

            // API リクエスト URI を出力（デバッグ）
            Debug.WriteLine($"API Get:{this.BuildUri(request)}");

            try
            {
                // ServiceUnavailableRetryCount 回までリトライする
                // リトライしても失敗した場合には例外が再スローされる
                RestResponse restResponse = await Policy.HandleResult<RestResponse>(x => IsRetryCondition(x))
                    .RetryAsync(ServiceUnavailableRetryCount)
                    .ExecuteAsync(async () =>
                    {
                        // API実行
                        return await this.GetAsync(request);
                    });

                if (!restResponse.IsSuccessful)
                {
                    // ネットワーク でエラーが起きた場合の対応
                    var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                    apiErrorResponse.Message = "サーバー呼出でエラーが発生しました。";
                    apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = restResponse.ErrorMessage, ValidationErrors = null };
                    return apiErrorResponse;
                }

                if (DumpResponse)
                {
                    // デバッグコンソールに応答内容を出力
                    Debug.WriteLine(restResponse.Content);
                }

                return JsonConvert.DeserializeObject<ApiResponse<T>>(restResponse.Content, _serializerSettings);
            }
            catch (Exception e) when (e is HttpException || e is HttpRequestException || e is OperationCanceledException)
            {
                // クライアントでエラーが起きた場合の対応
                var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                apiErrorResponse.Message = IsForbidden(e) ? "権限が不足しています。" : "ネットワークでエラーが発生しました。";
                apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = apiErrorResponse.Message, ValidationErrors = null };
                return apiErrorResponse;
            }
        }

        /// <summary>Getリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="parameter">パラメータ</param>
        /// <returns>レスポンス</returns>
        public Task<ApiResponse<T>> GetAsync<T>(string resource, object parameter)
            => GetAsync<T>(resource, ParameterToDictionary(parameter));

        /// <summary>
        /// ファイルをアップロードする（POST）
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<RestResponse> PostStreamAsync(string resource, string filePath)
        {
            var request = new RestRequest(resource, Method.Post);
            // リクエストヘッダの設定
            request.AddFile("formFile", filePath, "multipart/form-data");
            var result = await this.ExecuteAsync(request);
            return result;
        }

        /// <summary>
        /// ファイルをダウンロード
        /// </summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="parameters">パラメータ</param>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync(string resource, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var request = SetQueryParameter(new RestRequest(resource), parameters);
            // リクエストヘッダの設定
            AddRequestHeader(HttpMethod.Get, resource, request);
            // API実行
            return await this.DownloadStreamAsync(request);
        }

        #endregion リクエスト送信（非同期）

        #region リクエスト送信（同期）

        /// <summary>POST/PUT/DELETEなどのリクエストボディがあるリクエストを送信する</summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="method">Httpメソッド</param>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        protected RestResponse Execute(HttpMethod method, string resource, IEnumerable<KeyValuePair<string, object>> queryParameters, object body)
        {
            var request = SetQueryParameter(new RestRequest(resource), queryParameters);

            // リクエストボディの設定
            request.AddBody(body ?? new { });
            // リクエストヘッダの設定
            AddRequestHeader(method, resource, request);

            // API実行
            Func<RestClient, RestRequest, RestResponse> executeCore =
                method == HttpMethod.Post ? RestClientExtensions.Post :
                method == HttpMethod.Put ? RestClientSyncExtensions.Put :
                method == HttpMethod.Delete ? RestClientSyncExtensions.Delete : null;

            // ServiceUnavailableRetryCount 回までリトライする
            // リトライしても失敗した場合には例外が再スローされる
            RestResponse restResponse = Policy.HandleResult<RestResponse>(x => IsRetryCondition(x))
                .Retry(ServiceUnavailableRetryCount)
                .Execute(() =>
                {
                    return executeCore(this, request);
                });

            if (DumpResponse)
            {
                // デバッグコンソールに応答内容を出力
                Debug.WriteLine(restResponse.Content);
            }
            return restResponse;
        }

        /// <summary>POST/PUT/DELETEなどのリクエストボディがあるリクエストを送信する</summary>
        /// <typeparam name="T">戻り値の型</typeparam>
        /// <param name="method">Httpメソッド</param>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        protected ApiResponse<T> Execute<T>(HttpMethod method, string resource, IEnumerable<KeyValuePair<string, object>> queryParameters, object body)
        {
            //https://learn.microsoft.com/ja-jp/dotnet/fundamentals/networking/http/httpclient#http-error-handling

            try
            {
                RestResponse restResponse = Execute(method, resource, queryParameters, body);

                if (!restResponse.IsSuccessful)
                {
                    // ネットワーク でエラーが起きた場合の対応
                    var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                    apiErrorResponse.Message = "サーバー呼出でエラーが発生しました。";
                    apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = restResponse.ErrorMessage, ValidationErrors = null };
                    return apiErrorResponse;
                }

                return JsonConvert.DeserializeObject<ApiResponse<T>>(restResponse.Content, _serializerSettings);
            }
            catch (Exception e) when (e is HttpException || e is HttpRequestException || e is OperationCanceledException)
            {
                // クライアントでエラーが起きた場合の対応
                var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                apiErrorResponse.Message = IsForbidden(e) ? "権限が不足しています。" : "ネットワークでエラーが発生しました。";
                apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = apiErrorResponse.Message, ValidationErrors = null };
                return apiErrorResponse;
            }
        }

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Post<T>(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => Execute<T>(HttpMethod.Post, resource, queryParameters, body);

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Post<T>(string resource, object body)
            => Execute<T>(HttpMethod.Post, resource, null, body);

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        public void Post(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => Execute(HttpMethod.Post, resource, queryParameters, body);

        /// <summary>Postリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        public void Post(string resource, object body)
            => Execute(HttpMethod.Post, resource, null, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Put<T>(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => Execute<T>(HttpMethod.Put, resource, queryParameters, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Put<T>(string resource, object body)
            => Execute<T>(HttpMethod.Put, resource, null, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        public void Put(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => Execute(HttpMethod.Put, resource, queryParameters, body);

        /// <summary>Putリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        public void Put(string resource, object body)
            => Execute(HttpMethod.Put, resource, null, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Delete<T>(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => Execute<T>(HttpMethod.Put, resource, queryParameters, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Delete<T>(string resource, object body)
            => Execute<T>(HttpMethod.Put, resource, null, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="queryParameters">クエリパラメータ</param>
        /// <param name="body">リクエストボディ</param>
        public void Delete(string resource, IEnumerable<KeyValuePair<string, object>> queryParameters = null, object body = null)
            => Execute(HttpMethod.Put, resource, queryParameters, body);

        /// <summary>Deleteリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="body">リクエストボディ</param>
        public void Delete(string resource, object body)
            => Execute(HttpMethod.Put, resource, null, body);

        /// <summary>Getリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="parameters">パラメータ</param>
        /// <remarks><paramref name="parameters"/>は<see cref="Dictionary{TKey, TValue}"/>を推奨</remarks>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Get<T>(string resource, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var request = SetQueryParameter(new RestRequest(resource), parameters);

            // リクエストヘッダの設定
            AddRequestHeader(HttpMethod.Get, resource, request);

            try
            {
                // ServiceUnavailableRetryCount 回までリトライする
                // リトライしても失敗した場合には例外が再スローされる
                RestResponse restResponse = Policy.HandleResult<RestResponse>(x => IsRetryCondition(x))
                    .Retry(ServiceUnavailableRetryCount)
                    .Execute(() =>
                    {
                        // API実行
                        return this.Get(request);
                    });

                if (!restResponse.IsSuccessful)
                {
                    // ネットワーク でエラーが起きた場合の対応
                    var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                    apiErrorResponse.Message = "サーバー呼出でエラーが発生しました。";
                    apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = restResponse.ErrorMessage, ValidationErrors = null };
                    return apiErrorResponse;
                }

                if (DumpResponse)
                {
                    // デバッグコンソールに応答内容を出力
                    Debug.WriteLine(restResponse.Content);
                }

                return JsonConvert.DeserializeObject<ApiResponse<T>>(restResponse.Content, _serializerSettings);
            }
            catch (Exception e) when (e is HttpException || e is HttpRequestException || e is OperationCanceledException)
            {
                // クライアントでエラーが起きた場合の対応
                var apiErrorResponse = new ApiErrorResponse<T>(ApiResultType.ClientNetworkError);
                apiErrorResponse.Message = IsForbidden(e) ? "権限が不足しています。" : "ネットワークでエラーが発生しました。";
                apiErrorResponse.Errors = new Errors() { StatusCode = null, StatusMessage = apiErrorResponse.Message, ValidationErrors = null };
                return apiErrorResponse;
            }
        }

        /// <summary>Getリクエストを発行する</summary>
        /// <param name="resource">リクエスト先</param>
        /// <param name="parameter">パラメータ</param>
        /// <returns>レスポンス</returns>
        public ApiResponse<T> Get<T>(string resource, object parameter)
            => Get<T>(resource, ParameterToDictionary(parameter));

        #endregion リクエスト送信（同期）

        #region その他のメソッド

        /// <summary>クエリパラメータの設定</summary>
        /// <param name="request">リクエスト</param>
        /// <param name="parameters">パラメータ</param>
        /// <returns>リクエスト</returns>
        private RestRequest SetQueryParameter(RestRequest request, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            if (parameters != null)
            {
                foreach (var p in parameters.Where(p => p.Value != null))
                {
                    if (p.Value.GetType().IsArray)
                    {
                        // 同一のパラメータ名に複数の値を設定すると配列になる
                        Array.ForEach((object[])p.Value, x => request.AddQueryParameter(p.Key, x.ToString()));
                    }
                    else
                    {
                        request.AddQueryParameter(p.Key, p.Value?.ToString());
                    }
                }
            }
            return request;
        }

        /// <summary>パラメータをDictionaryに変換する</summary>
        /// <param name="parameter">パラメータ</param>
        /// <returns>Dictionary</returns>
        private IEnumerable<KeyValuePair<string, object>> ParameterToDictionary(object parameter)
        {
            return parameter.GetType().GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(parameter))
                // nullは除外
                .Where(kvp => kvp.Value != null)
                // stringなら空白も除外
                .Where(kvp => kvp.Value is not string s || !string.IsNullOrEmpty(s));
        }

        #endregion その他のメソッド
    }
}