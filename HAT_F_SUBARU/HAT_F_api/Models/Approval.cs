using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 承認データ:申請者が申請：新規レコード作成
/// 承認者のアクション：承認状態を更新
/// </summary>
public partial class Approval
{
    /// <summary>
    /// 承認要求番号
    /// </summary>
    public string ApprovalId { get; set; }

    /// <summary>
    /// 承認種別:仕入売上訂正/返品入力/返品入庫
    /// </summary>
    public string ApprovalType { get; set; }

    /// <summary>
    /// 承認対象ID
    /// </summary>
    public string ApprovalTargetId { get; set; }

    /// <summary>
    /// 承認状態,0:申請中 1:承認中 9:承認済
    /// </summary>
    public int? ApprovalStatus { get; set; }

    /// <summary>
    /// 申請者:社員ID
    /// </summary>
    public int RequestorEmpId { get; set; }

    /// <summary>
    /// 承認者1:社員ID
    /// </summary>
    public int? Approver1EmpId { get; set; }

    /// <summary>
    /// 承認者2:社員ID
    /// </summary>
    public int? Approver2EmpId { get; set; }

    /// <summary>
    /// 最終承認者:社員ID
    /// </summary>
    public int FinalApproverEmpId { get; set; }

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
