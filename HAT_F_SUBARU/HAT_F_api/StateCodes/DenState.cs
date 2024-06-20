namespace HAT_F_api.StateCodes
{
    /// <summary>伝票発注状態</summary>
    public static class DenState
    {
        /// <summary>未確定</summary>
        public static readonly string Pending = "0";

        /// <summary>発注済</summary>
        public static readonly string Ordered = "1";

        /// <summary>一部回答済</summary>
        public static readonly string Mix = "2";

        /// <summary>回答済</summary>
        public static readonly string Answered = "3";

        /// <summary>照合済（旧一貫化用）</summary>
        public static readonly string Collation = "4";

        /// <summary>手配済</summary>
        public static readonly string Arranged = "5";
    }
}