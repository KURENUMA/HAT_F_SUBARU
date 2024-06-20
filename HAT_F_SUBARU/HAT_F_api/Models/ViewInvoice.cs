using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewInvoice
{
    public string 得意先コード { get; set; }

    public string 得意先名 { get; set; }

    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 担当 { get; set; }

    public decimal? 売上金額合計 { get; set; }

    public decimal? 消費税合計 { get; set; }

    public short? 請求状態 { get; set; }

    public DateTime? 売上日 { get; set; }

    public string 備考 { get; set; }
}
