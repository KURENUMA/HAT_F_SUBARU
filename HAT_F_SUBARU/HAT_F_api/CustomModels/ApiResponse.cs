namespace HAT_F_api.CustomModels
{
    /// <summary>API実行結果</summary>
    /// <typeparam name="T">データ部の型</typeparam>
    public class ApiResponse<T>
    {
        public ApiResultType ResultType { get; set; } = ApiResultType.NotSet;

        /// <summary>
        /// システム的なエラー内容（ユーザー向けメッセージではありません）
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// ユーザーが解決可能なエラーの場合などにクライアントユーザーに伝えるエラーメッセージ
        /// </summary>
        public string ForUserMessage { get; set; } = "";

        public Errors Errors { get; set; } = new Errors();

        /// <summary>データ部</summary>
        public T Data { get; set; } = default;
    }

    public class ApiResponse : ApiResponse<object> { }
}
