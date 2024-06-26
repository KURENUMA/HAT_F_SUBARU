using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewInvoiceBatch
{
    public string 売上番号 { get; set; }

    public short 売上行番号 { get; set; }

    public short? 売上区分 { get; set; }

    public DateTime? 売上日 { get; set; }

    public decimal 売上単価 { get; set; }

    public int 売上数量 { get; set; }

    public string 顧客コード { get; set; }

    public short? 顧客枝番 { get; set; }

    public string 請求先コード { get; set; }

    public short? 顧客請求区分 { get; set; }

    public short? 顧客締日 { get; set; }

    public decimal? 前回入金額 { get; set; }

    public decimal? 前回請求残高 { get; set; }
}
