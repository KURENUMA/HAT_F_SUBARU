namespace HAT_F_api.CustomModels
{
    /// <summary>API実行結果（エラー）</summary>
    /// <typeparam name="T">データ部</typeparam>
    public class ApiErrorResponse<T> : ApiResponse<T>
    {
        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        public ApiErrorResponse(ApiResultType apiResultType)
        {
            ResultType = apiResultType;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        /// <param name="message">エラーメッセージ</param>
        public ApiErrorResponse(ApiResultType apiResultType, string message)
        {
            ResultType = apiResultType;
            Message = message;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="validationErrors">エラー詳細</param>
        public ApiErrorResponse(ApiResultType apiResultType, string message, IEnumerable<ValidationError> validationErrors = null)
        {
            ResultType = apiResultType;
            Message = message;

            Errors = new Errors()
            {
                StatusMessage = message,
                ValidationErrors = validationErrors?.ToList(),
            };
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="errors">エラー情報</param>
        public ApiErrorResponse(ApiResultType apiResultType, string message, Errors errors)
        {
            ResultType = apiResultType;
            Message = message;
            Errors = errors;
        }
    }

    public class ApiErrorResponse : ApiResponse
    {
        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        public ApiErrorResponse(ApiResultType apiResultType)
        {
            ResultType = apiResultType;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        /// <param name="message">エラーメッセージ</param>
        public ApiErrorResponse(ApiResultType apiResultType, string message)
        {
            ResultType = apiResultType;
            Message = message;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="validationErrors">エラー詳細</param>
        public ApiErrorResponse(ApiResultType apiResultType, string message, IEnumerable<ValidationError> validationErrors = null)
        {
            ResultType = apiResultType;
            Message = message;

            Errors = new Errors()
            {
                StatusMessage = message,
                ValidationErrors = validationErrors?.ToList(),
            };
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="apiResultType">API エラー種別</param>
        /// <param name="message">エラーメッセージ</param>
        /// <param name="errors">エラー情報</param>
        public ApiErrorResponse(ApiResultType apiResultType, string message, Errors errors)
        {
            ResultType = apiResultType;
            Message = message;
            Errors = errors;
        }
    }
}