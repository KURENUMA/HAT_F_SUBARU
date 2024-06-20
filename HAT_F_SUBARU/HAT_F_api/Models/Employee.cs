using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 社員マスタ
/// </summary>
public partial class Employee
{
    public int EmpId { get; set; }

    /// <summary>
    /// 社員コード
    /// </summary>
    public string EmpCode { get; set; }

    /// <summary>
    /// 社員名
    /// </summary>
    public string EmpName { get; set; }

    /// <summary>
    /// 社員名カナ
    /// </summary>
    public string EmpKana { get; set; }

    public string EmpTag { get; set; }

    /// <summary>
    /// パスワード (不使用)
    /// </summary>
    public string LoginPassword { get; set; }

    /// <summary>
    /// 電話番号
    /// </summary>
    public string Tel { get; set; }

    /// <summary>
    /// FAX番号
    /// </summary>
    public string Fax { get; set; }

    /// <summary>
    /// メールアドレス
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 部門コード
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 開始日
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 職種コード
    /// </summary>
    public string OccuCode { get; set; }

    /// <summary>
    /// 役職コード
    /// </summary>
    public string TitleCode { get; set; }

    /// <summary>
    /// 承認権限コード
    /// </summary>
    public string ApprovalCode { get; set; }

    public bool Deleted { get; set; }

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
