namespace HAT_F_api.StateCodes
{
    /// <summary>ヘッダー部分に対する発注状態</summary>
    public static class HeaderOrderState
    {
        /// <summary>未確定</summary>
        public static readonly string Pending = "0";

        /// <summary>発注済</summary>
        public static readonly string Ordered = "1";

        /// <summary>照合済</summary>
        public static readonly string Collation = "2";
    }
}