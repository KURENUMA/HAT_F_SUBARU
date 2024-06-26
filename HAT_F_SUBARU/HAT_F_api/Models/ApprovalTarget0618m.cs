using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ApprovalTarget0618m
{
    public string ApprovalTargetId { get; set; }

    public short ApprovalTargetSub { get; set; }

    public string SupCode { get; set; }

    public short? SupSubNo { get; set; }

    public decimal? PoPrice { get; set; }

    public string SalesNo { get; set; }

    public short? SalesRowNo { get; set; }

    public int? SalesQuantity { get; set; }

    public int? CorrectionQuantity { get; set; }

    public decimal? SalesUnitPrice { get; set; }

    public string CompCode { get; set; }

    public string SalesCd { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
