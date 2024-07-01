namespace HAT_F_api.Services
{
    /// <summary>
    /// シーケンス番号種別
    /// </summary>
    public enum SequenceNumber
    {
        /// <summary>
        /// 受発注テーブル.受注番号
        /// </summary>
        [TargetSequenceObject("FOS_JYUCHU_H_ORDER_NO")]
        FosJyuchuHOrderNo,

        /// <summary>
        /// 受発注テーブル.伝No
        /// </summary>
        [TargetSequenceObject("FOS_JYUCHU_H_DEN_NO")]
        FosJyuchuHDenNo,

        /// <summary>
        /// 受発注テーブル.DSEQ
        /// </summary>
        [TargetSequenceObject("FOS_JYUCHU_H_DSEQ")]
        FosJyuchuHDSeq,

        /// <summary>
        /// 請求テーブル.請求番号
        /// </summary>
        [TargetSequenceObject("INVOICE_INVOICE_NO")]
        InvoiceInvoiceNo,

        /// <summary>
        /// 仕入テーブル.仕入番号
        /// </summary>
        [TargetSequenceObject("PU_PU_NO")]
        PuPuNo,

        /// <summary>
        /// 売上テーブル.売上番号
        /// </summary>
        [TargetSequenceObject("SALES_SALES_NO")]
        SalesSalesNo,

        /// <summary>
        /// 返品データ.返品データID
        /// </summary>
        [TargetSequenceObject("RETURNING_PRODUCTS_RETURNING_PRODUCTS_ID")]
        ReturningProductsReturningProductsID,

        /// <summary>
        /// 在庫補充・発注.在庫補充・発注NO
        /// </summary>
        [TargetSequenceObject("STOCK_REFILL_ORDER_STOCK_REFILL_ORDER_NO")]
        StockRefillOrderStockRefillOrderNo,

        /// <summary>仕入取込データ</summary>
        [TargetSequenceObject("PU_IMPORT_PU_IMPORT_NO")]
        PuImportNo,

        /// <summary>売上調整番号</summary>
        [TargetSequenceObject("PURCHASE_ORDERS_PO_NO")]
        SalesAdjustmentNo,

        /// <summary>
        /// 発注データ.発注番号
        /// </summary>
        [TargetSequenceObject("SALES_ADJUSTMENT_SALES_ADJUSTMENT_NO")]
        SalesAdjustmentSalesAdjustmentNo,

        /// <summary>
        /// 発注データ明細.発注番号
        /// </summary>
        [TargetSequenceObject("PO_DETAILS_PO_NO")]
        PoDetailsPoNo,
    }
}
