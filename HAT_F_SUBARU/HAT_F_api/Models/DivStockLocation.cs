using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫置場区分★
/// </summary>
public partial class DivStockLocation
{
    /// <summary>
    /// 在庫置場倉庫CD
    /// </summary>
    public string DivStockWhCode { get; set; }

    /// <summary>
    /// 在庫置場CD
    /// </summary>
    public string DivStockLocationCode { get; set; }

    /// <summary>
    /// 在庫置場名
    /// </summary>
    public string DivStockLocationName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool? Deleted { get; set; }

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
