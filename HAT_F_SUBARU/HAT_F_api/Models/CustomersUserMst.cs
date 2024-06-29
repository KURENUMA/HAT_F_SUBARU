using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 顧客担当者マスタ★
/// </summary>
public partial class CustomersUserMst
{
    /// <summary>
    /// 顧客コード,取引先CD6桁:KOJICD 13桁 + 予備
    /// </summary>
    public string CustCode { get; set; }

    /// <summary>
    /// 担当者コード (キーマンCD)
    /// </summary>
    public string CustUserCode { get; set; }

    /// <summary>
    /// 担当者名 (キーマン名)
    /// </summary>
    public string CustUserName { get; set; }

    /// <summary>
    /// 担当者メールアドレス
    /// </summary>
    public string CustUserEmail { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool? Deleted { get; set; }

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
