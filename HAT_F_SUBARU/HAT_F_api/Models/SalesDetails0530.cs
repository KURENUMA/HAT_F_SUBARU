using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 売上データ明細
/// </summary>
public partial class SalesDetails0530
{
    /// <summary>
    /// 売上番号
    /// </summary>
    public string SalesNo { get; set; }

    /// <summary>
    /// 売上行番号
    /// </summary>
    public short RowNo { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 商品名
    /// </summary>
    public string ProdName { get; set; }

    /// <summary>
    /// 販売単価
    /// </summary>
    public decimal Unitprice { get; set; }

    /// <summary>
    /// 出荷数量
    /// </summary>
    public short? DeliveredQty { get; set; }

    /// <summary>
    /// 売上数量
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// 値引金額
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// 請求日
    /// </summary>
    public DateTime? InvoicedDate { get; set; }

    /// <summary>
    /// 請求番号
    /// </summary>
    public string InvoiceNo { get; set; }

    /// <summary>
    /// 請求遅延区分
    /// </summary>
    public short? InvoiceDelayType { get; set; }

    /// <summary>
    /// 自動仕訳処理日
    /// </summary>
    public DateTime? AutoJournalDate { get; set; }

    /// <summary>
    /// 伝票番号
    /// </summary>
    public string DenNo { get; set; }

    /// <summary>
    /// 伝区
    /// </summary>
    public string DenFlg { get; set; }

    /// <summary>
    /// HAT注文番号
    /// </summary>
    public string HatOrderNo { get; set; }

    /// <summary>
    /// H注番
    /// </summary>
    public string Chuban { get; set; }

    /// <summary>
    /// 品目
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 勘定科目
    /// </summary>
    public string AccountTitle { get; set; }

    /// <summary>
    /// 元売上番号
    /// </summary>
    public string OriginalSalesNo { get; set; }

    /// <summary>
    /// 元売上行番号
    /// </summary>
    public short? OriginalRowNo { get; set; }

    /// <summary>
    /// 承認対象ID
    /// </summary>
    public string ApprovalTargetId { get; set; }

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
