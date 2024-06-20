using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewProductStock
{
    public string データ種別 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public string 商品名フル { get; set; }

    public string 商品分類コード { get; set; }

    public string 商品分類名 { get; set; }

    public int 期首残高 { get; set; }

    public int? 入庫計 { get; set; }

    public int? 出庫計 { get; set; }

    public int 期首残高バラ { get; set; }

    public int? 入庫計バラ { get; set; }

    public int? 出庫計バラ { get; set; }

    public int 期末評価価格 { get; set; }

    public DateTime? 入出庫日時 { get; set; }
}
