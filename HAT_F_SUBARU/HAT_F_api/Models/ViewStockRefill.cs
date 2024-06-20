using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewStockRefill
{
    public string 倉庫コード { get; set; }

    public string 倉庫名 { get; set; }

    public string 商品コード { get; set; }

    public string 商品分類コード { get; set; }

    public string 商品分類名 { get; set; }

    public string ランク { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public short 実在庫数 { get; set; }

    public short 有効在庫数 { get; set; }

    public short? 在庫閾値 { get; set; }

    public short? 発注数基準 { get; set; }

    public short? 発注済数 { get; set; }

    public DateTime? 発注日時 { get; set; }
}
