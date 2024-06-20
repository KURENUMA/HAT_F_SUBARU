using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 入金データ
/// </summary>
public partial class Credit
{
    /// <summary>
    /// 入金番号
    /// </summary>
    public string CreditNo { get; set; }

    /// <summary>
    /// 入金日
    /// </summary>
    public DateTime? CreditDate { get; set; }

    /// <summary>
    /// 部門コード
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 開始日
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 顧客コード
    /// </summary>
    public string CompCode { get; set; }

    /// <summary>
    /// 顧客枝番
    /// </summary>
    public short? CompSubNo { get; set; }

    /// <summary>
    /// 支払方法区分,1:振込,2:手形,3:でんさい
    /// </summary>
    public short? PayMethodType { get; set; }

    /// <summary>
    /// 入金口座コード
    /// </summary>
    public string BankAcutCode { get; set; }

    /// <summary>
    /// 入金金額
    /// </summary>
    public decimal? ReceivedAmnt { get; set; }

    /// <summary>
    /// 消込金額
    /// </summary>
    public decimal? Received { get; set; }

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
