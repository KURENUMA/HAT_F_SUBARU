using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewPuDetail
{
    public string SupCode { get; set; }

    public string PaySupCode { get; set; }

    public string SlipComment { get; set; }

    public string PuNo { get; set; }

    public short PuRowNo { get; set; }

    public string PuRowDspNo { get; set; }

    public DateTime Noubi { get; set; }

    public string HatOrderNo { get; set; }

    public string PoRowNo { get; set; }

    public string ProdCode { get; set; }

    public string WhCode { get; set; }

    public string ProdName { get; set; }

    public decimal? PoPrice { get; set; }

    public decimal PuQuantity { get; set; }

    public short? TaxRate { get; set; }

    public string TaxFlg { get; set; }

    public short? CheckStatus { get; set; }

    public string PuKbn { get; set; }

    public DateTime? PuPayYearMonth { get; set; }

    public DateTime? PuPayDate { get; set; }

    public string PuCorrectionType { get; set; }

    public string OriginalPuNo { get; set; }

    public short? OriginalPuRowNo { get; set; }

    public string ApprovalTargetId { get; set; }
}
