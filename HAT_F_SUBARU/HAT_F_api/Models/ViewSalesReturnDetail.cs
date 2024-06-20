using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewSalesReturnDetail
{
    public string Hat注文番号 { get; set; }

    public string 伝票番号 { get; set; }

    public string 売上番号 { get; set; }

    public short 売上行番号 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public int 元数量 { get; set; }

    public int 現在数量 { get; set; }

    public decimal 単価 { get; set; }

    public decimal? 元合計金額 { get; set; }

    public decimal? 現在合計金額 { get; set; }

    public string 売区 { get; set; }

    public string 売区名 { get; set; }

    public int? 返品依頼数量 { get; set; }

    public int? 返品後数量 { get; set; }

    public decimal? 在庫単価 { get; set; }

    public string 返品id { get; set; }

    public short? 返品行番号 { get; set; }

    public string 承認要求番号 { get; set; }
}
