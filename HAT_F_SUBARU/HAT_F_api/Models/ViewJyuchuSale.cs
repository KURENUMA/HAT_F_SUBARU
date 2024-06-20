using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewJyuchuSale
{
    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public decimal? 販売単価 { get; set; }

    public int 出荷数量 { get; set; }

    public int? 売上数量 { get; set; }

    public int 値引金額 { get; set; }

    public DateTime? 請求日 { get; set; }

    public string 受注番号 { get; set; }

    public string 取引先コード { get; set; }

    public string 社員コード { get; set; }

    public string 部門コード { get; set; }

    public DateTime? 部門開始日 { get; set; }
}
