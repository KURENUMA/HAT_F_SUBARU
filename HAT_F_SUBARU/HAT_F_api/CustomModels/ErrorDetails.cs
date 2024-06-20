namespace HAT_F_api.CustomModels
{
    /// <summary>エラー詳細</summary>
    public class ErrorDetails
    {
        /// <summary>エラーコード</summary>
        public string StatusCode { get; set; }

        /// <summary>エラーメッセージ</summary>
        public string StatusMessage { get; set; }

        /// <summary>バリデーションエラー</summary>
        public List<ValidationError> ValidationErrors { get; set; }
    }
}