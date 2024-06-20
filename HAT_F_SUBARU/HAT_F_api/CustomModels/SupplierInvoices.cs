namespace HAT_F_api.CustomModels
{
    public class SupplierInvoices
    {
        /// <summary>
        /// 受注番号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 伝票番号
        /// </summary>
        public string denNo { get; set; }
        /// <summary>
        /// 仕入先コード
        /// </summary>
        public string ShiresakiCd { get; set; }
        /// <summary>
        /// 仕入先名
        /// </summary>
        public string ShiresakiName { get; set; }
        /// <summary>
        /// 発注合計金額
        /// </summary>
        public decimal OrderAmnt { get; set; }
        /// <summary>
        /// 消費税記号
        /// </summary>
        public string CmpTax { get; set; }
        /// <summary>
        /// 納期
        /// </summary>
        public DateTime Nouki { get; set; }
        /// <summary>
        /// 入力日
        /// </summary>
        public DateTime InpDate { get; set; }
        /// <summary>
        /// 入力者
        /// </summary>
        public string InpName { get; set; }
        /// <summary>
        /// 営業担当
        /// </summary>
        public string HatSales { get; set; }
        /// <summary>
        /// 得意先
        /// </summary>
        public string Tokuisaki { get; set; }
        /// <summary>
        /// 得意先の営業担当
        /// </summary>
        public string TokuisakiSales { get; set; }
        /// <summary>
        /// 請求書確認：0:未確認,1:確認済,2:編集中,3:違算
        /// </summary>
        public short InvoiceConfirmation { get; set; }
        /// <summary>
        /// 仕入先支払月：0:当月,1:翌月,2:翌々月
        /// </summary>
        public short ShiresakiPaymentMonth { get; set; }
        /// <summary>
        /// 仕入先支払日：10:10日払い,99：末日
        /// </summary>
        public short ShiresakiPaymentDay { get; set; }
        /// <summary>
        /// 仕入先支払い区分：1:振込,2:手形
        /// </summary>
        public short ShiresakiPaymentClassification { get; set; }
    }
}
