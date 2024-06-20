using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewInvoicedAmount
{
    public string 請求番号 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先名 { get; set; }

    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 担当 { get; set; }

    public decimal? 当月入金額 { get; set; }

    public DateTime? 請求日 { get; set; }

    public decimal? 請求額 { get; set; }

    public decimal? 消費税金額 { get; set; }

    public short? 請求状態 { get; set; }
}
