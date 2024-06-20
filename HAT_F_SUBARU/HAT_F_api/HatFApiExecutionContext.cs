namespace HAT_F_api
{
    /// <summary>
    /// API実行コンテキスト
    /// </summary>
    public class HatFApiExecutionContext
    {
        private static readonly TimeZoneInfo jstZoneInfo = System.TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        public HatFApiExecutionContext() 
        {
            this.ExecuteDateTimeUtc = DateTime.Now.ToUniversalTime();
        }

        /// <summary>
        /// アプリケーション実行日時(UTC)
        /// </summary>
        public DateTime ExecuteDateTimeUtc { get; set; }

        /// <summary>
        /// アプリケーション実行日時(JST)
        /// </summary>
        /// <remarks>
        /// システム時刻が欲しいときはDateTime.Now等を直接使用せず、こちらを参照してください
        /// </remarks>
        public DateTime ExecuteDateTimeJst
        {
            get
            {
                DateTime jstDateTime = System.TimeZoneInfo.ConvertTimeFromUtc(this.ExecuteDateTimeUtc, jstZoneInfo);
                return jstDateTime;
            }
        }
    }
}
