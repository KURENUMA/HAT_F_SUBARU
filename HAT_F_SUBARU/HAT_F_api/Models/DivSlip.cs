using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 伝票区分
/// </summary>
public partial class DivSlip
{
    /// <summary>
    /// 伝票区分CD
    /// </summary>
    public string SlipCd { get; set; }

    /// <summary>
    /// 伝票区分名
    /// </summary>
    public string SlipName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
