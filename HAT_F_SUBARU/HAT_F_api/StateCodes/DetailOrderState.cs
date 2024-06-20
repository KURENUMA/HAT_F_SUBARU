namespace HAT_F_api.StateCodes
{
    /// <summary>受注明細情報に対する発注状態</summary>
    public static class DetailOrderState
    {
        /// <summary>未確定</summary>
        public static readonly string Pending = "0";

        /// <summary>発注済</summary>
        public static readonly string Ordered = "1";

        /// <summary>回答済み</summary>
        public static readonly string Answered = "2";

        /// <summary>照合済</summary>
        public static readonly string Collation = "3";
    }
}