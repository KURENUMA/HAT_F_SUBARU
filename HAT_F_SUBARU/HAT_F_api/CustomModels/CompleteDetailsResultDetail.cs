namespace HAT_F_api.CustomModels
{
    /// <summary>受注情報明細部分の補完結果（details部分）</summary>
    public class CompleteDetailsResultDetail
    {
        /// <summary>子番</summary>
        public short? Koban {  get; set; }

        /// <summary>商品分類Cd</summary>
        public string SyohinCd { get; set; }

        /// <summary>バラ数</summary>
        public int? Bara { get; set; }

        /// <summary>在庫数</summary>
        public short? Stock { get; set; }

        /// <summary>売上単価種別</summary>
        public string UriageTankaType { get; set; }

        /// <summary>売掛金</summary>
        public decimal? Urikake { get; set; }

        /// <summary>売上単価</summary>
        public decimal? UriageTanka { get; set; }

        /// <summary>売上金額</summary>
        public decimal? UriageKingaku { get; set; }

        /// <summary>仕入れ掛け金</summary>
        public decimal? SiireKake { get; set; }

        /// <summary>仕入れ単価</summary>
        public decimal? SiireTanka { get; set; }

        /// <summary>仕入金額</summary>
        public decimal? SiireKingaku { get; set; }

        /// <summary>仕入回答単価</summary>
        public decimal? ShiireKaitouTanka { get; set; }

        /// <summary>定価単価</summary>
        public decimal? TeikaTanka { get; set; }

        /// <summary>定価金額</summary>
        public decimal? TeikaKingaku { get; set; }
    }
}