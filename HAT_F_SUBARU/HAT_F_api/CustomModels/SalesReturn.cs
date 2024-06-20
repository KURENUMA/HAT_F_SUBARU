namespace HAT_F_api.CustomModels
{
    public class SalesReturn
    {

        /// <summary>
        /// 返品ID
        /// </summary>
        public string ReturningProductsId { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        public short? RowNo { get; set; }

        /// <summary>
        /// 売上番号
        /// </summary>
        public string SalesNo { get; set; }

        /// <summary>
        /// 売上行番号
        /// </summary>
        public short? SalesRowNo { get; set; }

        /// <summary>
        /// 伝票番号
        /// </summary>
        public string DenNo { get; set; }

        /// <summary>
        /// 商品コード
        /// </summary>
        public string ProdCode { get; set; }

        /// <summary>
        /// 売上数量
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// 返品依頼数量
        /// </summary>
        public int? ReturnRequestQuantity { get; set; }

        /// <summary>
        /// 返品数量
        /// </summary>
        public int? ReturnQuantity { get; set; }

        /// <summary>
        /// 返金単価
        /// </summary>
        public decimal? RefundUnitPrice { get; set; }

        /// <summary>
        /// 売上単価
        /// </summary>
        public decimal? SellUnitPrice { get; set; }

        /// <summary>
        /// 在庫単価
        /// </summary>
        public decimal? StockUnitPrice { get; set; }

        /// <summary>
        /// 売区
        /// </summary>
        public string SalesCd { get; set; }

    }
}
