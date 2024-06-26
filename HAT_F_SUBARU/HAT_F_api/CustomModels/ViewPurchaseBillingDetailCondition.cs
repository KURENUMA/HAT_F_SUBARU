namespace HAT_F_api.CustomModels
{
    public class ViewPurchaseBillingDetailCondition
    {
        public string 物件コード { get; set; }

        public string 物件名 { get; set; }

        public string 得意先コード { get; set; }

        public string 得意先 { get; set; }

        public string Hat注文番号 { get; set; }

        public string 仕入先コード { get; set; }

        public string 仕入先 { get; set; }

        public string 伝票番号 { get; set; }

        public string 伝票区分 { get; set; }

        public string H注番 { get; set; }

        public DateTime? H納日 { get; set; }

        public short 売上確定 { get; set; }

        public string 商品コード { get; set; }

        public string 商品名 { get; set; }

        public int? H数量 { get; set; }

        public decimal? H単価 { get; set; }

        public decimal? H金額 { get; set; }

        public DateTime? M納日 { get; set; }

        public string M伝票番号 { get; set; }

        public string M注番 { get; set; }

        public short? M数量 { get; set; }

        public decimal? M単価 { get; set; }

        public decimal? M金額 { get; set; }

        public short? 照合ステータス { get; set; }

        public string H行番号 { get; set; }

        public string Hページ番号 { get; set; }

        public string 伝区 { get; set; }

        public string 仕入番号 { get; set; }

        public short? 仕入行番号 { get; set; }

        public DateTime? 仕入支払年月日 { get; set; }

        public string 部門コード { get; set; }

        public DateTime? 納日From { get; set; }

        public DateTime? 納日To { get; set; }

        public DateTime? 仕入支払年月日From { get; set; }

        public DateTime? 仕入支払年月日To { get; set; }
    }
}
