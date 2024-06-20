using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 発注区分
/// </summary>
public partial class DivOrder
{
    /// <summary>
    /// 発注区分CD
    /// </summary>
    public string OrderCd { get; set; }

    /// <summary>
    /// 発注区分名
    /// </summary>
    public string OrderName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
