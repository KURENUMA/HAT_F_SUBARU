using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 承認処理
/// </summary>
public partial class ApprovalProcedure
{
    /// <summary>
    /// 承認処理ID
    /// </summary>
    public string ApprovalProcedureId { get; set; }

    /// <summary>
    /// 承認要求番号
    /// </summary>
    public string ApprovalId { get; set; }

    /// <summary>
    /// 社員ID
    /// </summary>
    public int EmpId { get; set; }

    /// <summary>
    /// 承認動作,0:申請 1:差し戻し 2:承認済 3:最終承認済
    /// </summary>
    public int ApprovalResult { get; set; }

    /// <summary>
    /// 承認コメント
    /// </summary>
    public string ApprovalComment { get; set; }

    /// <summary>
    /// 登録日
    /// </summary>
    public DateTime ApprovalDate { get; set; }

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
