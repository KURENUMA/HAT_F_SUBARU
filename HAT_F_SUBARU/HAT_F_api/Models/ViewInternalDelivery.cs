using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewInternalDelivery
{
    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 取引先コード { get; set; }

    public string 取引先名 { get; set; }

    public string 伝票番号 { get; set; }

    public decimal? 利率 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public decimal? 販売単価 { get; set; }

    public int? 販売数量 { get; set; }

    public decimal? 販売額 { get; set; }

    public decimal? 仕入単価 { get; set; }

    public short? 仕入数量 { get; set; }

    public decimal? 仕入額 { get; set; }
}
