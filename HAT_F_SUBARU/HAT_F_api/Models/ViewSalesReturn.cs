using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewSalesReturn
{
    public string Hat注文番号 { get; set; }

    public string 伝票番号 { get; set; }

    public string 返品id { get; set; }

    public string 承認要求番号 { get; set; }

    public int 承認ステータス区分 { get; set; }

    public string 承認ステータス { get; set; }
}
