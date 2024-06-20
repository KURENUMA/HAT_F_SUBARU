namespace HAT_F_api.CustomModels
{
    /// <summary>受注情報明細部分の補完の入力パラメータ（details部分）</summary>
    public class CompleteDetailsRequestDetail
    {
        /// <summary>子番</summary>
        public short? Koban { get; set; }

        /// <summary>倉庫コード</summary>
        public string SokoCd { get; set; }

        /// <summary>商品Cd</summary>
        public string SyohinCd { get; set; }

        /// <summary>売上区分</summary>
        public string UriKubun { get; set; }

        /// <summary>売上記号</summary>
        public string UriageKigou { get; set; }

        /// <summary>売上掛け率</summary>
        public decimal? UriageKakeritsu { get; set; }

        /// <summary>売上単価</summary>
        public decimal? UriageTanka { get; set; }

        /// <summary>仕入記号</summary>
        public string ShiireKigou { get; set; }

        /// <summary>仕入掛け率</summary>
        public decimal? ShiireKakeritsu { get; set; }

        /// <summary>仕入単価</summary>
        public decimal? ShiireTanka { get; set; }

        /// <summary>仕入回答単価</summary>
        public decimal? ShiireKaitouTanka { get; set; }

        /// <summary>定価単価</summary>
        public decimal? TeikaTanka { get; set; }

        /// <summary>数量</summary>
        public int? Suuryo { get; set; }

        /// <summary>単位</summary>
        public string Tani { get; set; }

        /// <summary>バラ数</summary>
        public int? BaraCount { get; set; }
    }
}