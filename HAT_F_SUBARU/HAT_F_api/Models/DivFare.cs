using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 運賃区分
/// </summary>
public partial class DivFare
{
    /// <summary>
    /// 運賃区分CD
    /// </summary>
    public string FareCd { get; set; }

    /// <summary>
    /// 運賃区分名
    /// </summary>
    public string FareName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
