using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入金額照合ロック
/// </summary>
public partial class AmounCheckLock
{
    /// <summary>
    /// HAT注文番号
    /// </summary>
    public string HatOrderNo { get; set; }

    /// <summary>
    /// 編集者
    /// </summary>
    public int? EditorEmpId { get; set; }

    /// <summary>
    /// 編集開始日時
    /// </summary>
    public DateTime? EditStartDatetime { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public int? Updater { get; set; }
}
