namespace HAT_F_api.CustomModels
{
    /// <summary>バリデーションエラー</summary>
    public class ValidationError
    {
        /// <summary>該当項目名</summary>
        public string Field { get; set; }

        /// <summary>メッセージ</summary>
        public string Description { get; set; }
    }
}