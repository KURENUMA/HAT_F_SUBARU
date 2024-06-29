using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 入金口座マスタ
/// </summary>
public partial class BankAcutMstNew
{
    /// <summary>
    /// 入金口座コード
    /// </summary>
    public string BankAcutCode { get; set; }

    /// <summary>
    /// 入金口座名
    /// </summary>
    public string ReciveActName { get; set; }

    /// <summary>
    /// 適用開始日
    /// </summary>
    public DateTime ApplStartDate { get; set; }

    /// <summary>
    /// 適用終了日
    /// </summary>
    public DateTime? ApplEndDate { get; set; }

    /// <summary>
    /// 適用開始後入金口座名
    /// </summary>
    public string StartActName { get; set; }

    /// <summary>
    /// 入金口座区分,B:銀行 P:郵便局
    /// </summary>
    public string ReciveBankActType { get; set; }

    /// <summary>
    /// 入金口座番号,銀行:7桁 郵便局:12桁
    /// </summary>
    public string ReciveActNo { get; set; }

    /// <summary>
    /// 銀行口座種別,O:普通 C:当座
    /// </summary>
    public string BankActType { get; set; }

    /// <summary>
    /// 口座名義人
    /// </summary>
    public string ActName { get; set; }

    /// <summary>
    /// 部門コード
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 部門開始日
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 全銀協銀行コード
    /// </summary>
    public string ABankCode { get; set; }

    /// <summary>
    /// 全銀協支店コード
    /// </summary>
    public string ABankBlncCode { get; set; }

    /// <summary>
    /// プログラム更新日時
    /// </summary>
    public DateTime? UpdatePlgDate { get; set; }

    /// <summary>
    /// 更新プログラム名
    /// </summary>
    public string UpdatePgm { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public string Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public string Updater { get; set; }
}
