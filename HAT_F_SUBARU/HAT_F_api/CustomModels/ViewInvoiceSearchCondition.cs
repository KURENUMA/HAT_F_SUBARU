using System.ComponentModel;

namespace HAT_F_api.CustomModels
{
    /// <summary>請求一覧の検索条件</summary>
    public class ViewInvoiceSearchCondition
    {
        /// <summary>物件コード</summary>
        [DisplayName("物件コード")]
        public int ConstructionCode { get; set; }

        /// <summary>物件名</summary>
        [DisplayName("物件名")]
        public string ConstructionName { get; set; }

        /// <summary>得意先コード</summary>
        [DisplayName("得意先コード")]
        public string TokuiCode { get; set; }

        /// <summary>得意先</summary>
        [DisplayName("得意先")]
        public string TokuiName { get; set; }

        /// <summary>請求金額合計</summary>
        [DisplayName("請求金額合計")]
        public int InvoiceAmnt { get; set; }

        /// <summary>消費税総額</summary>
        [DisplayName("消費税総額")]
        public int CmpTaxAmnt { get; set; }

        /// <summary>営業担当</summary>
        [DisplayName("営業担当")]
        public string EnpName { get; set; }

        /// <summary>ステータス</summary>
        [DisplayName("ステータス")]
        public string InvoiceState { get; set; }

        /// <summary>顧客支払月</summary>
        [DisplayName("顧客支払月")]
        public short CustPayMonths { get; set; }

        /// <summary>顧客支払日</summary>
        [DisplayName("顧客支払日")]
        public short CustPayDates { get; set; }

        /// <summary>顧客支払方法</summary>
        [DisplayName("顧客支払方法")]
        public short CustPayMethod { get; set; }
    }
}
