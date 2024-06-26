using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class CorrectionDeliveryCheck
{
    public int CorrectionDeliveryCheckId { get; set; }

    public DateTime? CheckDatetime { get; set; }

    public int? CheckerId { get; set; }

    public string CheckerPost { get; set; }

    public string SalesNo { get; set; }

    public short? RowNo { get; set; }

    public string Comment { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? Updater { get; set; }
}
