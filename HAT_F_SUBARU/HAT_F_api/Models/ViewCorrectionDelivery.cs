using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewCorrectionDelivery
{
    public string 得意先コード { get; set; }

    public string 得意先名 { get; set; }

    public DateTime? 訂正日 { get; set; }

    public decimal? 売上金額 { get; set; }
}
