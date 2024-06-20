namespace HAT_F_api.CustomModels
{
    /// <summary>売上確定前利率異常チェック結果</summary>
    public class InterestRateCheckBeforeFixResult
    {
        /// <summary>受注明細のSAVE_KEY</summary>
        public string SaveKey { get; set; }

        /// <summary>受注明細のDEN_SORT</summary>
        public string DenSort { get; set; }

        /// <summary>受注明細のDEN_NO_LINE</summary>
        public string DenNoLine { get; set; }

        /// <summary>チェック者名</summary>
        public string Checker { get; set; }

        /// <summary>チェック者役職</summary>
        public string CheckerPost { get; set; }

        /// <summary>コメント</summary>
        public string Comment { get; set; }
    }
}