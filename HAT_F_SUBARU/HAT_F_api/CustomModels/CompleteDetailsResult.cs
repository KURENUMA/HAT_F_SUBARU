namespace HAT_F_api.CustomModels
{
    /// <summary>受注情報明細部分の補完結果</summary>
    public class CompleteDetailsResult
    {
        /// <summary>明細部分</summary>
        public List<CompleteDetailsResultDetail> Details { get; set; }

        /// <summary>定価金額合計</summary>
        public decimal? TeikaKingakuSum { get; set; }

        /// <summary>売上金額合計</summary>
        public decimal? UriageKingakuSum { get; set; }

        /// <summary>仕入金額合計</summary>
        public decimal? ShiireKingakuSum { get; set; }

        /// <summary>粗利</summary>
        public decimal? Profit { get; set; }

        /// <summary>粗利率</summary>
        public decimal? ProfitRate { get; set; }
    }
}