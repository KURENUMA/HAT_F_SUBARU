using AutoMapper;
using HAT_F_api.CustomModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace HatFClient.Common
{
    [DebuggerStepThrough]
    internal class ApiHelper
    {
        private static Regex _urlParamRegex = new Regex(@"\{([^\/]+)\}");

        /// <summary>
        /// API処理結果が成功系かどうかを返します
        /// </summary>
        /// <returns>成功系の場合は true, 失敗系の場合 false</returns>
        public static bool IsPositiveResponse<T>(ApiResponse<T> apiResponse)
        {
            if (apiResponse == null)
            {
                return false;
            }

            switch (apiResponse.ResultType)
            {
                case ApiResultType.Success:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// API処理結果が失敗系かどうかを返します
        /// </summary>
        /// <returns>失敗系の場合は true, 成功系の場合 false</returns>
        public static bool IsNegativeResponse<T>(ApiResponse<T> apiResponse)
        {
            bool isPositive = IsPositiveResponse(apiResponse);
            return !isPositive;
        }

        /// <summary>
        /// 参照(検索)系API呼出ラッパー。API呼出成否は成功失敗が分かれば十分なときに使用できます。
        /// </summary>
        public async static Task<ApiResult<T>> FetchAsync<T>(Form form, Func<Task<ApiResponse<T>>> fetchProccess)
        {
            ApiResponse<T> apiResponse;

            // プログレスバーを表示する処理
            using (var progressForm = new SimpleProgressForm())
            {
                // この using ブロック内には、画面にアクセス(参照/設定)するコードを記述しないでください
                progressForm.Start(form);

                apiResponse = await fetchProccess();
            }

            if (AfterDataFetchBehavior(form, apiResponse))
            {
                return new ApiResult<T>(true, apiResponse.Data, apiResponse);
            }
            else 
            {
                return new ApiResult<T>(false, default(T), null);
            }
        }

        /// <summary>
        /// 更新系API呼出ラッパー。API呼出成否は成功失敗が分かれば十分なときに使用できます。
        /// </summary>
        public async static Task<ApiResult<T>> UpdateAsync<T>(Form form, Func<Task<ApiResponse<T>>> updateProccess)
        {
            return await UpdateAsync(form, updateProccess, false);
        }

        /// <summary>
        /// 更新系API呼出ラッパー。API呼出成否は成功失敗が分かれば十分なときに使用できます。
        /// </summary>
        public async static Task<ApiResult<T>> UpdateAsync<T>(Form form, Func<Task<ApiResponse<T>>> updateProccess, bool silentWhenSuccess)
        {
            ApiResponse<T> apiResponse;

            // プログレスバーを表示する処理
            using (var progressForm = new SimpleProgressForm())
            {
                // この using ブロック内には、画面にアクセス(参照/設定)するコードを記述しないでください
                progressForm.Start(form);

                apiResponse = await updateProccess();
            }

            if (AfterDataUpdateBehavior(form, apiResponse, null, silentWhenSuccess))
            {
                return new ApiResult<T>(true, apiResponse.Data, apiResponse);
            }
            else
            {
                return new ApiResult<T>(false, default(T), null);
            }
        }


        /// <summary>
        /// データ取得系API呼出後の共通動作
        /// </summary>
        /// <param name="form">ロック対象ウィンドウ</param>
        /// <param name="apiResponse">API呼出結果</param>
        /// <returns>データ取得が成功なら true, 失敗なら false</returns>
        public static bool AfterDataFetchBehavior<T>(Form form, ApiResponse<T> apiResponse)
        {
            if (ApiHelper.IsNegativeResponse(apiResponse))
            {
                // データ取得失敗したのでエラーメッセージ
                DialogHelper.WarningMessage(form, apiResponse?.Message ?? "ネットワーク エラー");
                return false;
            }

            return true;
        }

        /// <summary>
        /// データ更新API呼出後の共通動作
        /// </summary>
        /// <param name="form">ロック対象ウィンドウ</param>
        /// <param name="apiResponse">API呼出結果</param>
        /// <param name="updateConflictReloadLogic">データ更新で楽観的排他ロックエラー発生時の画面のデータ再読み込みロジック</param>
        /// <returns>データ更新が成功なら true, 失敗なら false</returns>
        public static bool AfterDataUpdateBehavior<T>(Form form, ApiResponse<T> apiResponse, Action updateConflictReloadLogic)
        {
            return AfterDataUpdateBehavior(form, apiResponse, updateConflictReloadLogic, false);
        }

        /// <summary>
        /// データ更新API呼出後の共通動作
        /// </summary>
        /// <param name="form">ロック対象ウィンドウ</param>
        /// <param name="apiResponse">API呼出結果</param>
        /// <param name="updateConflictReloadLogic">データ更新で楽観的排他ロックエラー発生時の画面のデータ再読み込みロジック</param>
        /// <returns>データ更新が成功なら true, 失敗なら false</returns>
        public static bool AfterDataUpdateBehavior<T>(Form form, ApiResponse<T> apiResponse, Action updateConflictReloadLogic, bool silentWhenSuccess)
        {
            // このメソッドを修正するときは AfterDataUpdateBehaviorAsync メソッドも同様に修正すること

            if (IsPositiveResponse(apiResponse))
            {
                if (!silentWhenSuccess)
                {
                    // 更新OK
                    DialogHelper.InformationMessage(form, "更新しました。");
                }
                return true;
            }

            if (apiResponse.ResultType == ApiResultType.ApiDbUpdateConcurrencyError)
            {
                // 楽観的排他ロック エラー


                if (updateConflictReloadLogic == null)
                {
                    string message = "他のユーザーが同じデータを先に更新したため、更新できませんでした。";
                    DialogHelper.WarningMessage(form, message);
                }
                else
                {
                    string message = "他のユーザーが同じデータを先に更新したため、更新できませんでした。画面のデータを再読み込みします。";
                    if (DialogHelper.OkCancelWarning(form, message, true))
                    {
                        updateConflictReloadLogic();
                    }
                    else
                    {
                        message = "画面を開きなおすまで保存できません。ご注意ください。";
                        DialogHelper.InformationMessage(form, message);
                    }
                }

                return false;
            }

            // その他エラーの場合
            // 通信エラー等の場合は再度実行できるかもしれないので画面を閉じない
            DialogHelper.WarningMessage(form, apiResponse?.Message ?? "");
            return false;
        }

        /// <summary>
        /// データ更新API呼出後の共通動作
        /// </summary>
        /// <param name="form">ロック対象ウィンドウ</param>
        /// <param name="apiResponse">API呼出結果</param>
        /// <param name="updateConflictReloadLogic">データ更新で楽観的排他ロックエラー発生時の画面のデータ再読み込みロジック</param>
        /// <returns>データ更新が成功なら true, 失敗なら false</returns>
        public static async Task<bool> AfterDataUpdateBehaviorAsync<T>(Form form, ApiResponse<T> apiResponse, Func<Task> updateConflictReloadLogic)
        { 
            return await AfterDataUpdateBehaviorAsync(form, apiResponse, updateConflictReloadLogic, false);
        }

        /// <summary>
        /// データ更新API呼出後の共通動作
        /// </summary>
        /// <param name="form">ロック対象ウィンドウ</param>
        /// <param name="apiResponse">API呼出結果</param>
        /// <param name="updateConflictReloadLogic">データ更新で楽観的排他ロックエラー発生時の画面のデータ再読み込みロジック</param>
        /// <returns>データ更新が成功なら true, 失敗なら false</returns>
        public static async Task<bool> AfterDataUpdateBehaviorAsync<T>(Form form,ApiResponse<T> apiResponse, Func<Task> updateConflictReloadLogic, bool silentWhenSuccess)
        {
            // このメソッドを修正するときは AfterDataUpdateBehavior メソッドも同様に修正すること

            if (IsPositiveResponse(apiResponse))
            {
                if (!silentWhenSuccess)
                {
                    // 更新OK
                    DialogHelper.InformationMessage(form, "更新しました。");
                }
                return true;
            }

            if (apiResponse.ResultType == ApiResultType.ApiDbUpdateConcurrencyError)
            {
                // 楽観的排他ロック エラー


                if (updateConflictReloadLogic == null)
                {
                    string message = "他のユーザーが同じデータを先に更新したため、更新できませんでした。";
                    DialogHelper.WarningMessage(form, message);
                }
                else 
                {
                    string message = "他のユーザーが同じデータを先に更新したため、更新できませんでした。画面のデータを再読み込みします。";
                    if (DialogHelper.OkCancelWarning(form, message, true))
                    {
                        await updateConflictReloadLogic();
                    }
                    else
                    {
                        message = "画面を開きなおすまで保存できません。ご注意ください。";
                        DialogHelper.InformationMessage(form, message);
                    }
                }

                return false;
            }

            // その他エラーの場合
            // 通信エラー等の場合は再度実行できるかもしれないので画面を閉じない
            DialogHelper.WarningMessage(form, apiResponse.Message);
            return false;
        }
        public static string AddQuery(string url, string queryName, object queryValue)
        {
            string separator = url.Contains("?") ? "&" : "?";
            string newUrl = $"{url}{separator}{HttpUtility.UrlEncode(queryName)}={HttpUtility.UrlEncode(queryValue.ToString())}";
            return newUrl;
        }

        public static string AddPagingQuery(string url, int page, int pageSize)
        {
            string separator = url.Contains("?") ? "&" : "?";
            string newUrl = $"{url}{separator}rows={pageSize}&page={page}";
            return newUrl;
        }

        public static string AddUnlimitedQuery(string url)
        {
            string separator = url.Contains("?") ? "&" : "?";
            string newUrl = $"{url}{separator}rows={int.MaxValue}";
            return newUrl;
        }


        //public static ApiResponse<TTo> ConvertApiResponseDataType<TFrom, TTo>(ApiResponse<TFrom> source)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
        //    var mapper = new Mapper(config);
        //    var data = mapper.Map<TTo>(source.Data);

        //    if (IsPositiveResponse(source))
        //    {
        //        return new ApiOkResponse<TTo>(data);
        //    }
        //    else
        //    {
        //        return new ApiErrorResponse<TTo>(source.ResultType, source.Message, source.Errors);
        //    }
        //}

        public static ApiResponse<TTo> ConvertApiResponseDataType<TFrom, TTo>(ApiResponse<TFrom> source, Func<TFrom, TTo> convertor)
        {
            if (IsPositiveResponse(source))
            {
                TTo data;
                if (convertor != null)
                {
                    data = convertor(source.Data);
                }
                else
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
                    var mapper = new Mapper(config);
                    data = mapper.Map<TTo>(source.Data);
                }
                return new ApiOkResponse<TTo>(data);
            }
            else
            {
                return new ApiErrorResponse<TTo>(source.ResultType, source.Message, source.Errors);
            }
        }

        public static ApiResponse<List<TTo>> ConvertApiResponseDataType<TFrom, TTo>(ApiResponse<List<TFrom>> source, Func<List<TFrom>, List<TTo>> convertor)
        {
            if (IsPositiveResponse(source))
            {
                List<TTo> data;
                if (convertor != null)
                {
                    data = convertor(source.Data);
                }
                else
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>());
                    var mapper = new Mapper(config);
                    data = mapper.Map<List<TTo>>(source.Data);
                }
                return new ApiOkResponse<List<TTo>>(data);
            }
            else
            {
                return new ApiErrorResponse<List<TTo>>(source.ResultType, source.Message, source.Errors);
            }
        }

        public static string UrlEncodeForWebApi(string url)
        {
            var encodedUrl = new StringBuilder(HttpUtility.UrlEncode(url));
            encodedUrl.Replace("+", "%20");
            return encodedUrl.ToString();
        }

        /// <summary>
        /// 使用例: CreateResourceUrl("/api/anyapi/{p1}/{p2}/", new { p1 = "test", p2 = 123 });
        /// </summary>
        /// <param name="urlTemplate"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateResourceUrl(string urlTemplate, dynamic parameters)
        {
            return CreateResourceUrl(urlTemplate, parameters, true);
        }

        /// <summary>
        /// 使用例: CreateResourceUrl("/api/anyapi/{p1}/{p2}/", new { p1 = "test", p2 = 123 }, true);
        /// </summary>
        /// <param name="urlTemplate"></param>
        /// <param name="parameters"></param>
        /// <param name="paramUrlEncode">パラメータのURLエンコードを行うか</param>
        /// <returns></returns>
        public static string CreateResourceUrl(string urlTemplate, dynamic parameters, bool paramUrlEncode)
        {
            var sb = new StringBuilder(urlTemplate);
            var matches = _urlParamRegex.Matches(urlTemplate);

            Type type = parameters.GetType();

            foreach (Match match in matches)
            {
                //System.Diagnostics.Debug.WriteLine(match.Value);
                //System.Diagnostics.Debug.WriteLine(match.Groups[1]);

                string name = match.Groups[1].Value;
                PropertyInfo pi = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                object value = pi?.GetValue(parameters, null);
                string valueString = value?.ToString() ?? "";

                if (paramUrlEncode)
                {
                    valueString = UrlEncodeForWebApi(valueString);
                }

                sb.Replace(match.Value, valueString);
            }

            string url = sb.ToString();
            return url;
        }
    }
}
