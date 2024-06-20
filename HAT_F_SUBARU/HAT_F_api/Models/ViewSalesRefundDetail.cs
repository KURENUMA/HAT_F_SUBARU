using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewSalesRefundDetail
{
    public string Hat注文番号 { get; set; }

    public string 伝票番号 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public int? 数量 { get; set; }

    public decimal? 単価 { get; set; }

    public string 売区 { get; set; }

    public string 売区名 { get; set; }

    public int? 返品依頼数量 { get; set; }

    public int? 返品数量 { get; set; }

    public int? 返品後数量 { get; set; }

    public decimal? 返金単価 { get; set; }

    public int? 返金額 { get; set; }

    public decimal? 在庫単価 { get; set; }

    public string 返品id { get; set; }

    public short 返品行番号 { get; set; }

    public string 返金承認要求番号 { get; set; }
}
