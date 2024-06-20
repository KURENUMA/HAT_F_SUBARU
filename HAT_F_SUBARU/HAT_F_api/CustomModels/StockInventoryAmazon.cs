namespace HAT_F_api.CustomModels
{
    public class StockInventoryAmazon
    {
        public int? No { get; set; }

        /// <summary>
        /// 商品コード
        /// </summary>
        public string ProdCode { get; set; }

        /// <summary>
        /// HAT在庫数
        /// </summary>
        public int? StockHat { get; set; }

        /// <summary>
        /// AMAZON在庫数
        /// </summary>
        public int? StockAmazon { get; set; }

        /// <summary>
        /// 倉庫在庫数
        /// </summary>
        public int? StockWarehouse { get; set; }

        /// <summary>
        /// 棚卸在庫数
        /// </summary>
        public int? Inventory {  get; set; }

        /// <summary>
        /// 差異数
        /// </summary>
        public int? Difference { get; set; }

        /// <summary>
        /// 商品コードが在庫データ棚卸にあるか
        /// </summary>
        public bool ProdCodeExists {  get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }
    }
}
