using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class SalesDetails0618m
{
    public string SalesNo { get; set; }

    public short RowNo { get; set; }

    public string ProdCode { get; set; }

    public string ProdName { get; set; }

    public decimal Unitprice { get; set; }

    public short? DeliveredQty { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }

    public DateTime? InvoicedDate { get; set; }

    public string InvoiceNo { get; set; }

    public short? InvoiceDelayType { get; set; }

    public DateTime? AutoJournalDate { get; set; }

    public string DenNo { get; set; }

    public string DenFlg { get; set; }

    public string HatOrderNo { get; set; }

    public string Chuban { get; set; }

    public string Item { get; set; }

    public string Summary { get; set; }

    public string AccountTitle { get; set; }

    public string SalesCd { get; set; }

    public string SupCode { get; set; }

    public string SaveKey { get; set; }

    public string DenSort { get; set; }

    public string DenNoLine { get; set; }

    public string SalesCorrectionType { get; set; }

    public string OriginalSalesNo { get; set; }

    public short? OriginalRowNo { get; set; }

    public string ApprovalTargetId { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
