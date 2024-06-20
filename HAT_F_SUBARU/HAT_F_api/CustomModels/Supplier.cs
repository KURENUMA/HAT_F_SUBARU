namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// 仕入先
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// 仕入先CD
        /// </summary>
        public string SupplierCd { get; set; } = "";
        /// <summary>
        /// 仕入先名_漢字
        /// </summary>
        public string SupplierName { get; set; } = "";
        /// <summary>
        /// 仕入先名_カナ
        /// </summary>
        public string SupplierNameKana { get; set; } = "";
        /// <summary>
        /// FAX番号
        /// </summary>
        public string SupplierFax { get; set; } = "";
        /// <summary>
        /// 電話番号
        /// </summary>
        public string SupplierTel { get; set; } = "";
        /// <summary>
        /// 5桁コード
        /// </summary>
        public string CategoryCode { get; set; } = "";
        /// <summary>
        /// 商品分類名
        /// </summary>
        public string CategoryName { get; set; } = "";
    }
}
