using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 顧客MF支払引先
/// </summary>
public partial class CustomerMfPayee
{
    /// <summary>
    /// 顧客コード
    /// </summary>
    public string CustCode { get; set; }

    /// <summary>
    /// MF支払先コード
    /// </summary>
    public string MfPayeeCode { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
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
