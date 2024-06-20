using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫補充発注詳細★
/// </summary>
public partial class StockRefillOrderDetail
{
    /// <summary>
    /// 在庫補充発注ID
    /// </summary>
    public long StockRefillOrderId { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 発注個数
    /// </summary>
    public short? OrderQuantity { get; set; }

    /// <summary>
    /// 発注単価
    /// </summary>
    public decimal? OrderUnitPrice { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
