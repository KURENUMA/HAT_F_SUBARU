using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 売上データ調整
/// </summary>
public partial class SalesAdjustment
{
    /// <summary>
    /// 売上調整番号
    /// </summary>
    public string SalesAdjustmentNo { get; set; }

    /// <summary>
    /// 物件コード
    /// </summary>
    public string ConstructionCode { get; set; }

    /// <summary>
    /// 得意先コード
    /// </summary>
    public string TokuiCd { get; set; }

    /// <summary>
    /// 調整区分,1:協賛金 2:保険 3:経費 4:商品購入
    /// </summary>
    public short? AdjustmentCategory { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 勘定科目
    /// </summary>
    public string AccountTitle { get; set; }

    /// <summary>
    /// 調整金額
    /// </summary>
    public decimal? AdjustmentAmount { get; set; }

    /// <summary>
    /// 消費税:B:10%,8:8%,Z:非課税
    /// </summary>
    public string TaxFlg { get; set; }

    /// <summary>
    /// 消費税率
    /// </summary>
    public short? TaxRate { get; set; }

    /// <summary>
    /// 請求日
    /// </summary>
    public DateTime? InvoicedDate { get; set; }

    /// <summary>
    /// 社員ID
    /// </summary>
    public int? EmpId { get; set; }

    public string ApprovalId { get; set; }

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
