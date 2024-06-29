using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewFixedSale
{
    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先 { get; set; }

    public string 伝票番号 { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public DateTime? 納期 { get; set; }

    public string 営業担当者名 { get; set; }

    public long? 売上合計金額 { get; set; }

    public string Hat注文番号 { get; set; }
}
