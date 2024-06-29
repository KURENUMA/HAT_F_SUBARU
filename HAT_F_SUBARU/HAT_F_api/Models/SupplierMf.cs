using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入先マネーフォワード連携
/// </summary>
public partial class SupplierMf
{
    /// <summary>
    /// 顧客コード
    /// </summary>
    public string SupCode { get; set; }

    /// <summary>
    /// マネーフォワード仕入先コード
    /// </summary>
    public string MfSupCode { get; set; }

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
