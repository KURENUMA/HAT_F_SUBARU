using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 役割区分
/// </summary>
public partial class DivUserRole
{
    /// <summary>
    /// 役割CD
    /// </summary>
    public string UserRoleCd { get; set; }

    /// <summary>
    /// 役割区分名
    /// </summary>
    public string UserRoleName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
