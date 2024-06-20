using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewInterestRateFixed
{
    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 取引先コード { get; set; }

    public string 取引先名 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public decimal 販売単価 { get; set; }

    public int 数量 { get; set; }

    public decimal? 販売額 { get; set; }

    public string 摘要 { get; set; }

    public string 伝票番号 { get; set; }

    public decimal? 仕入単価 { get; set; }

    public short? 仕入数量 { get; set; }

    public decimal? 仕入額 { get; set; }

    public string 売上番号 { get; set; }

    public short 売上行番号 { get; set; }

    public decimal? 利率 { get; set; }

    public string コメント者 { get; set; }

    public string コメント役職 { get; set; }

    public string コメント { get; set; }
}
