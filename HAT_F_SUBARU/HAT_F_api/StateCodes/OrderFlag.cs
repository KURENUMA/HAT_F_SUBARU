namespace HAT_F_api.StateCodes
{
    /// <summary>受注フラグ</summary>
    public static class OrderFlag
    {
        /// <summary>通常受注</summary>
        public static readonly string Normal = "1";

        /// <summary>見積受注</summary>
        public static readonly string ViaEstimate = "2";

        /// <summary>OPS受注</summary>
        public static readonly string ViaOps = "3";

        /// <summary>請書取込</summary>
        public static readonly string ViaUkesyo = "5";

        /// <summary>エプコ取込</summary>
        public static readonly string ViaEpuko = "6";

        /// <summary>新OPS</summary>
        public static readonly string ViaNewOps = "7";
    }
}