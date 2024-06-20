using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 売上区分
/// </summary>
public partial class DivUriage
{
    /// <summary>
    /// 売上区分CD
    /// </summary>
    public string UriageCd { get; set; }

    /// <summary>
    /// 売上区分名
    /// </summary>
    public string UriageName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
