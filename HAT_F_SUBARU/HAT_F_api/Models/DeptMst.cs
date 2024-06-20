using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 部門マスタ
/// </summary>
public partial class DeptMst
{
    /// <summary>
    /// 部門コード
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 開始日
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 終了日
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 部門名
    /// </summary>
    public string DepName { get; set; }

    /// <summary>
    /// 組織階層
    /// </summary>
    public short DeptLayer { get; set; }

    /// <summary>
    /// 部門パス
    /// </summary>
    public string DeptPath { get; set; }

    /// <summary>
    /// 最下層区分
    /// </summary>
    public short Terminal { get; set; }

    /// <summary>
    /// チームコード
    /// </summary>
    public string TeamCd { get; set; }

    /// <summary>
    /// 伝票入力可否,0:不可 1:可能
    /// </summary>
    public short SlitYn { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
