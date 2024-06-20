namespace HAT_F_api.CustomModels
{
    /// <summary>売上確定前利率異常一覧の検索条件</summary>
    public class ViewInterestRateBeforeFixSearchCondition
    {
        public string 伝票番号 { get; set; }

        public string 伝票区分 { get; set; }

        public string 物件コード { get; set; }

        public string 物件名 { get; set; }

        public string 得意先コード { get; set; }

        public string 得意先 { get; set; }

        public string 商品コード { get; set; }

        public string 商品名 { get; set; }

        public long? 売上合計金額 { get; set; }

        public string 売上記号 { get; set; }

        public decimal? 売上単価 { get; set; }

        public decimal? 売上掛率 { get; set; }

        public string 仕入記号 { get; set; }

        public decimal? 仕入単価 { get; set; }

        public decimal? 仕入額 { get; set; }

        public decimal? 仕入掛率 { get; set; }

        public decimal? 定価 { get; set; }

        public string 仕入先コード { get; set; }

        public string 仕入先名 { get; set; }

        public string 営業担当者名 { get; set; }

        public string コメント役職 { get; set; }

        public string コメント者 { get; set; }
    }
}