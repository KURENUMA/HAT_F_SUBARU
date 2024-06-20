using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 請求データ明細
/// </summary>
public partial class InvoiceDetail
{
    /// <summary>
    /// 請求番号
    /// </summary>
    public string InvoiceNo { get; set; }

    /// <summary>
    /// 売上番号
    /// </summary>
    public string SalesNo { get; set; }

    /// <summary>
    /// 売上行番号
    /// </summary>
    public short RowNo { get; set; }

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
