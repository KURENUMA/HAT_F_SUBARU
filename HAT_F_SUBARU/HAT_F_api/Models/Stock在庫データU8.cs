using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class Stock在庫データU8
{
    public string WhCode { get; set; }

    public string ProdCode { get; set; }

    public string RotNo { get; set; }

    public string StockType { get; set; }

    public string QualityType { get; set; }

    public string StockRank { get; set; }

    public string Actual { get; set; }

    public string Valid { get; set; }

    public string EvaluationPrice { get; set; }

    public string AbcRank { get; set; }

    public string LastDeliveryDate { get; set; }

    public string RowVer { get; set; }

    public string CreateDate { get; set; }

    public string Creator { get; set; }

    public string UpdateDate { get; set; }

    public string Updater { get; set; }
}
