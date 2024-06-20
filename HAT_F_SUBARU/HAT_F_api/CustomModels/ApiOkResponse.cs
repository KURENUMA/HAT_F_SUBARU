namespace HAT_F_api.CustomModels
{
    /// <summary>API実行結果（正常）</summary>
    /// <typeparam name="T">データ部</typeparam>
    public class ApiOkResponse<T> : ApiResponse<T>
    {
        /// <summary>コンストラクタ</summary>
        /// <param name="data">データ部</param>
        public ApiOkResponse(T data)
        {
            this.ResultType = ApiResultType.Success;
            this.Data = data;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="data">データ部</param>
        /// <param name="message">メッセージ</param>
        public ApiOkResponse(T data, string message)
        {
            this.ResultType = ApiResultType.Success;
            this.Data = data;
            this.Message = message;
        }
    }

    /// <summary>API実行結果（正常）</summary>
    public class ApiOkResponse : ApiResponse 
    {
        /// <summary>コンストラクタ</summary>
        public ApiOkResponse()
        {
            this.ResultType = ApiResultType.Success;
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="message">メッセージ</param>
        public ApiOkResponse(string message)
        {
            this.ResultType = ApiResultType.Success;
        }
    }

}