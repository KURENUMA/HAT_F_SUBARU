using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewStockInventory
{
    public string 倉庫cd { get; set; }

    public string 倉庫名 { get; set; }

    public string 商品cd { get; set; }

    public string 在庫区分 { get; set; }

    public string 良品区分 { get; set; }

    public DateTime 棚卸年月 { get; set; }

    public string 在庫置場cd { get; set; }

    public string 在庫置場名 { get; set; }

    public int? 棚卸no { get; set; }

    public string ランク { get; set; }

    public short? 管理在庫数 { get; set; }

    public short? 棚卸在庫数 { get; set; }

    public short? 差異数 { get; set; }

    public string 状況 { get; set; }
}
