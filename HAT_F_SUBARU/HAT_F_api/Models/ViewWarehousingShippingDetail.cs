using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewWarehousingShippingDetail
{
    public string SaveKey { get; set; }

    public string DenSort { get; set; }

    public string DenNoLine { get; set; }

    public string 伝票番号 { get; set; }

    public string Hat商品コード { get; set; }

    public string 商品名 { get; set; }

    public int? 数量 { get; set; }

    public int? バラ { get; set; }
}
