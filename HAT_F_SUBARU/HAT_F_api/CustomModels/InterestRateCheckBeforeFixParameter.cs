namespace HAT_F_api.CustomModels
{
    /// <summary>売上確定前利率異常チェック記録用パラメータ</summary>
    public class InterestRateCheckBeforeFixParameter
    {
        /// <summary>受注明細のSAVE_KEY</summary>
        public string SaveKey { get; set; }

        /// <summary>受注明細のDEN_SORT</summary>
        public string DenSort { get; set; }

        /// <summary>受注明細のDEN_NO_LINE</summary>
        public string DenNoLine { get; set; }

        /// <summary>コメント</summary>
        public string Comment { get; set; }
    }
}