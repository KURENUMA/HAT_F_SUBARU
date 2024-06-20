namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product
    {
        /// <summary>
        /// HAT商品コード
        /// </summary>
        public string Cd24 { get; set; }
        /// <summary>
        /// メーカー商品コード
        /// </summary>
        public string MKey { get; set; }
        /// <summary>
        /// 商品分類コード
        /// </summary>
        public string Code5 { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        public string Nnm { get; set; }
        /// <summary>
        /// 商品名(カナ)
        /// </summary>
        public string Anm { get; set; }
        /// <summary>
        /// メーカCD
        /// </summary>
        public string MkCd { get; set; }
        /// <summary>
        /// メーカー商品情報
        /// </summary>
        public string MInfo { get; set; }
    }
}
