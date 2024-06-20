using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 役職既定役割 (ロール)
/// </summary>
public partial class TitleDefaultRole
{
    /// <summary>
    /// 役職コード
    /// </summary>
    public string TitleCode { get; set; }

    /// <summary>
    /// 役割CD
    /// </summary>
    public string UserRoleCd { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool? Deleted { get; set; }

    /// <summary>
    /// CREATE_DATE
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// CREATOR
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// UPDATE_DATE
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// UPDATER
    /// </summary>
    public int? Updater { get; set; }
}
