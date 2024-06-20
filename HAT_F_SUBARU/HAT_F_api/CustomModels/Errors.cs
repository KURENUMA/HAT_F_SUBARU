namespace HAT_F_api.CustomModels
{
    /// <summary>Api実行結果のエラー情報</summary>
    public class Errors
    {
        /// <summary>ステータスコード</summary>
        public string StatusCode { get; set; }

        /// <summary>メッセージ</summary>
        public string StatusMessage { get; set; }

        /// <summary>バリデーションエラー</summary>
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
