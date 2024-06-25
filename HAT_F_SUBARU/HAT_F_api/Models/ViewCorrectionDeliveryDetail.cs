using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewCorrectionDeliveryDetail
{
    public string 得意先コード { get; set; }

    public string 得意先名 { get; set; }

    public DateTime? 訂正日 { get; set; }

    public string 訂正種別 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public string 元売上番号 { get; set; }

    public short 元売上行番号 { get; set; }

    public int 元売上数量 { get; set; }

    public decimal 元売上単価 { get; set; }

    public int? 元売上利率 { get; set; }

    public string 訂正番号 { get; set; }

    public short 訂正行番号 { get; set; }

    public int 訂正数量 { get; set; }

    public decimal 訂正単価 { get; set; }

    public int? 訂正利率 { get; set; }

    public string 部門コード { get; set; }

    public string 部門名 { get; set; }

    public int? 訂正申請者id { get; set; }

    public string 訂正申請者名 { get; set; }

    public string 確認者 { get; set; }

    public string 確認者役職 { get; set; }

    public string コメント { get; set; }
}
