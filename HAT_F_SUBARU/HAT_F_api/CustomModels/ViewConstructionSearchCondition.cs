using System.ComponentModel;

namespace HAT_F_api.CustomModels
{
    /// <summary>物件一覧の検索条件</summary>
    public class ViewConstructionSearchCondition
    {
        /// <summary>物件コード</summary>
        [DisplayName("物件コード")]
        public string ConstructionCode { get; set; }

        /// <summary>物件名</summary>
        [DisplayName("物件名")]
        public string ConstructionName { get; set; }

        /// <summary>受注状態</summary>
        [DisplayName("受注状態")]
        public short OrderState { get; set; }

        /// <summary>得意先コード</summary>
        [DisplayName("得意先コード")]
        public string TokuiCode { get; set; }

        /// <summary>得意先</summary>
        [DisplayName("得意先")]
        public string TokuiName { get; set; }

        /// <summary>登録者</summary>
        [DisplayName("登録者")]
        public string EnpName { get; set; }

        /// <summary>現場郵便番号</summary>
        [DisplayName("現場郵便番号")]
        public string RecvPostcode { get; set; }

        /// <summary>現場住所1</summary>
        [DisplayName("現場住所1")]
        public string RecvAdd1 { get; set; }

        /// <summary>現場住所2</summary>
        [DisplayName("現場住所2")]
        public string RecvAdd2 { get; set; }

        /// <summary>現場住所3</summary>
        [DisplayName("現場住所3")]
        public string RecvAdd3 { get; set; }

        /// <summary>引合日</summary>
        [DisplayName("引合日")]
        public DateTime InquiryDate { get; set; }

        /// <summary>建設会社名</summary>
        [DisplayName("建設会社名")]
        public string ConstructorName { get; set; }

        /// <summary>備考</summary>
        [DisplayName("備考")]
        public string Comment { get; set; }
    }
}
