using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewPurchaseBilling
{
    public string 照合ステータス { get; set; }

    public string F仕入先コード { get; set; }

    public string F仕入先 { get; set; }

    public string 伝票番号 { get; set; }

    public string 伝区 { get; set; }

    public DateTime? F納日 { get; set; }

    public string F注文番号 { get; set; }

    public string F注番 { get; set; }

    public string F商品コード { get; set; }

    public string F商品名 { get; set; }

    public int? F数量 { get; set; }

    public decimal? F単価 { get; set; }

    public decimal? F金額 { get; set; }

    public short? F消費税率 { get; set; }

    public string M仕入先コード { get; set; }

    public string M納品書番号 { get; set; }

    public DateTime? M納入日 { get; set; }

    public string M注文番号 { get; set; }

    public string M注番 { get; set; }

    public string M商品コード { get; set; }

    public string M商品名 { get; set; }

    public decimal? M数量 { get; set; }

    public decimal? M金額 { get; set; }

    public short? M消費税率 { get; set; }
}
