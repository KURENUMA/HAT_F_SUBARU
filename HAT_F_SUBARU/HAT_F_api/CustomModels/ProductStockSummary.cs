namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// 商品在庫情報（在庫全体）
    /// </summary>
    public class ProductStockSummary
    {
        /// <summary>
        /// 期首残高
        /// </summary>
        public int BeginningBalance { get; set; }

        /// <summary>
        /// 入庫計（入庫数量計）
        /// </summary>
        public int StockReceiptTotal { get; set; }

        /// <summary>
        /// 出庫計（出庫数量計）
        /// </summary>
        public int StockOutputTotal { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 総商品数
        /// </summary>
        public int ProductCountTotal { get; set; }
    }
}
