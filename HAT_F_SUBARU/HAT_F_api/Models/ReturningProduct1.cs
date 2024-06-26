using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 返品データ★
/// </summary>
public partial class ReturningProduct1
{
    /// <summary>
    /// 返品ID
    /// </summary>
    public string ReturningProductsId { get; set; }

    /// <summary>
    /// 返品日時
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    /// <summary>
    /// 返品入庫確認状況
    /// </summary>
    public int? ReturnConfirmationStatus { get; set; }

    /// <summary>
    /// 返品入庫確認者ID
    /// </summary>
    public int? ReturnConfirmationId { get; set; }

    /// <summary>
    /// 返品入庫確認日時
    /// </summary>
    public DateTime? ReturnConfirmationDate { get; set; }

    /// <summary>
    /// 承認要求番号
    /// </summary>
    public string ApprovalId { get; set; }

    /// <summary>
    /// 入庫承認要求番号
    /// </summary>
    public string StockApprovalId { get; set; }

    /// <summary>
    /// 返金承認要求番号
    /// </summary>
    public string RefundApprovalId { get; set; }

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
