namespace HAT_F_api.CustomModels
{
    /// <summary>
    /// 商品在庫情報（個別）
    /// </summary>
    public class ProductStock
    {
        /// <summary>
        /// 商品コード
        /// PROD_CODE
        /// </summary>
        public string ProdCode { get; set; } = "";

        /// <summary>
        /// 商品名
        /// PROD_NAME
        /// </summary>
        public string ProdName { get; set; } = "";

        /// <summary>
        /// 商品名（フル）
        /// PROD_FULLNAME
        /// </summary>
        public string ProdFullName { get; set; } = "";

        /// <summary>
        /// 商品分類コード
        /// CATEGORY_CODE
        /// </summary>
        public string CategoryCode { get; set; } = "";

        /// <summary>
        /// 商品分類名
        /// PROD_CATE_NAME
        /// </summary>
        public string ProdCategoryName { get; set; } = "";

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
        /// 期末評価価格
        /// </summary>
        public int PeriodEndEvaluationPrice { get; set; }
    }
}
