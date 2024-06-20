﻿using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewSalesCorrectionDetail
{
    public string 仕入先伝票 { get; set; }

    public string 伝票番号 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public int? H数量 { get; set; }

    public int M納品単価 { get; set; }

    public decimal? H納品金額 { get; set; }

    public int M納品金額 { get; set; }
}
