using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 顧客マネーフォワード連携
/// </summary>
public partial class CustomersMf
{
    /// <summary>
    /// 顧客コード
    /// </summary>
    public string CustCode { get; set; }

    /// <summary>
    /// マネーフォワード顧客コード
    /// </summary>
    public string MfCustCode { get; set; }

    /// <summary>
    /// マネーフォワード支払先コード
    /// </summary>
    public string MfPayeeCode { get; set; }

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
