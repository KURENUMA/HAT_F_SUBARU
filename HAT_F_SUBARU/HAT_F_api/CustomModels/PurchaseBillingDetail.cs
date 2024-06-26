using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class PurchaseBillingDetail
    {
        public string Hat注文番号 { get; set; }

        public string 仕入先コード { get; set; }

        public string 支払先コード { get; set; }

        public short? 仕入先コード枝番 { get; set; }

        public string 仕入先 { get; set; }

        public string H注番 { get; set; }

        public string 商品コード { get; set; }

        public string 商品名 { get; set; }

        public int H数量 { get; set; }

        public decimal? H単価 { get; set; }

        public DateTime? M納日 { get; set; }

        public string 区分 { get; set; }

        public string H伝票番号 { get; set; }

        public string M伝票番号 { get; set; }

        public string M注番 { get; set; }

        public decimal M数量 { get; set; }

        public decimal? M単価 { get; set; }

        public short? 照合ステータス { get; set; }

        public string 倉庫コード { get; set; }

        public string H行番号 { get; set; }

        public string Hページ番号 { get; set; }

        public int 社員Id { get; set; }

        public string 部門コード { get; set; }

        public string 備考 { get; set; }

        public string 仕入番号 { get; set; }

        public short? 仕入行番号 { get; set; }

        public string 伝区 { get; set; }

        public string 納品書番号 { get; set; }

        public string 子番 { get; set; }

        public string 消費税 { get; set; }

        public DateTime? 支払日 { get; set; }
    }
}
