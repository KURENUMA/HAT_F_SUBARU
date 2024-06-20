using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 請求データ
/// </summary>
public partial class Invoice
{
    /// <summary>
    /// 請求番号
    /// </summary>
    public string InvoiceNo { get; set; }

    /// <summary>
    /// 請求日
    /// </summary>
    public DateTime? InvoicedDate { get; set; }

    /// <summary>
    /// 取引先コード
    /// </summary>
    public string CompCode { get; set; }

    /// <summary>
    /// 顧客枝番
    /// </summary>
    public short? CustSubNo { get; set; }

    /// <summary>
    /// 請求状態: 0:未請求/1:請求書発行済/2:請求書送付済/3:入金済/4:完了
    /// </summary>
    public short? InvoiceState { get; set; }

    /// <summary>
    /// 前回入金額
    /// </summary>
    public decimal? LastReceived { get; set; }

    /// <summary>
    /// 当月売上額
    /// </summary>
    public decimal? MonthSales { get; set; }

    /// <summary>
    /// 当月入金額
    /// </summary>
    public decimal? MonthReceived { get; set; }

    /// <summary>
    /// 当月請求額
    /// </summary>
    public decimal? MonthInvoice { get; set; }

    /// <summary>
    /// 消費税金額
    /// </summary>
    public decimal CmpTax { get; set; }

    /// <summary>
    /// 請求消込金額
    /// </summary>
    public decimal? InvoiceReceived { get; set; }

    /// <summary>
    /// 前回請求残高
    /// </summary>
    public decimal? PreviousInvoice { get; set; }

    /// <summary>
    /// 当月残高
    /// </summary>
    public decimal? CurrentBalance { get; set; }

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
