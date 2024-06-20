using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 返品データ明細★
/// </summary>
public partial class ReturningProductsDetail
{
    /// <summary>
    /// 返品ID
    /// </summary>
    public string ReturningProductsId { get; set; }

    /// <summary>
    /// 行番号
    /// </summary>
    public short RowNo { get; set; }

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
    /// 販売単価
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

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
