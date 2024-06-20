using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewReturnReceipt
{
    public string Hat注番 { get; set; }

    public string 伝票番号 { get; set; }

    public string 仕入先名 { get; set; }

    public string 発送元 { get; set; }

    public string 受注番号 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public int? H数量 { get; set; }

    public int 返品数量 { get; set; }

    public decimal? H納品単価 { get; set; }

    public int 返品合計 { get; set; }
}
