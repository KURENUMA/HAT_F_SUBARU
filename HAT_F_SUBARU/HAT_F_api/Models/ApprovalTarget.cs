using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 承認対象★
/// </summary>
public partial class ApprovalTarget
{
    /// <summary>
    /// 承認対象ID
    /// </summary>
    public string ApprovalTargetId { get; set; }

    /// <summary>
    /// 承認対象枝番
    /// </summary>
    public short ApprovalTargetSub { get; set; }

    /// <summary>
    /// 仕入番号
    /// </summary>
    public string PuNo { get; set; }

    /// <summary>
    /// 仕入行番号
    /// </summary>
    public short? PuRowNo { get; set; }

    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string SupCode { get; set; }

    /// <summary>
    /// 仕入先枝番
    /// </summary>
    public short? SupSubNo { get; set; }

    /// <summary>
    /// 仕入単価（M単価）
    /// </summary>
    public decimal? PoPrice { get; set; }

    /// <summary>
    /// 仕入数量（M数量）
    /// </summary>
    public short? PuQuantity { get; set; }

    /// <summary>
    /// 売上番号
    /// </summary>
    public string SalesNo { get; set; }

    /// <summary>
    /// 売上行番号
    /// </summary>
    public short? RowNo { get; set; }

    /// <summary>
    /// 売上数量
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// 販売単価
    /// </summary>
    public decimal? Unitprice { get; set; }

    /// <summary>
    /// 取引先コード
    /// </summary>
    public string CompCode { get; set; }

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
