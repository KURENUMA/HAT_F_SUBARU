namespace HAT_F_api.CustomModels
{
    /// <summary>受注情報明細部分の補完の入力パラメータ（pages部分）</summary>
    public class CompleteDetailsRequestPage
    {
        /// <summary>発注方法</summary>
        public int Hkbn { get; set; }

        /// <summary>受注区分（受区）（1:通常受注, 2:見積受注, 3:ＯＰＳ, 4:HOPE, 5:請書取込, 6:エプコ取込, 7:新ＯＰＳ, 8:ＯＣＲ）</summary>
        public string OrderFlag { get; set; }

        /// <summary>伝票区分</summary>
        public string DenFlag { get; set; }

        /// <summary>伝票発注状態</summary>
        public string OrderState { get; set; }

        /// <summary>伝区</summary>
        public string DenCd { get; set; }

        /// <summary>納期</summary>
        public string Nouki { get; set; }

        /// <summary>得意先Cd</summary>
        public string TokuiCd { get; set; }

        /// <summary>チームCd</summary>
        public string TeamCd { get; set; }

        /// <summary>明細部分</summary>
        public IEnumerable<CompleteDetailsRequestDetail> Details { get; set; }

        /// <summary>定価金額合計</summary>
        public decimal TeikaKingakuSum { get; set; }

        /// <summary>売上金額合計</summary>
        public decimal UriageKingakuSum { get; set; }

        /// <summary>仕入金額合計</summary>
        public decimal ShiireKingakuSum { get; set; }

        /// <summary>粗利</summary>
        public decimal Profit { get; set; }

        /// <summary>粗利率</summary>
        public decimal ProfitRate { get; set; }
    }
}