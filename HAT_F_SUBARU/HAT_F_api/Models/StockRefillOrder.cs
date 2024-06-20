using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫補充発注★
/// </summary>
public partial class StockRefillOrder
{
    /// <summary>
    /// 在庫補充発注ID
    /// </summary>
    public long StockRefillOrderId { get; set; }

    /// <summary>
    /// 倉庫コード
    /// </summary>
    public string WhCode { get; set; }

    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string SupCode { get; set; }

    /// <summary>
    /// 発注日時
    /// </summary>
    public DateTime? OrderDatetime { get; set; }

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
