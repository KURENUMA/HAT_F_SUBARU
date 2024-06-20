using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewWarehousingReceivingDetail
{
    public string 伝票番号 { get; set; }

    public string H注番 { get; set; }

    public string Hat商品コード { get; set; }

    public string 商品名 { get; set; }

    public int? 入庫予定数量 { get; set; }

    public int? 入庫数量 { get; set; }

    public int? 入庫予定バラ数量 { get; set; }

    public int? 入庫バラ数量 { get; set; }

    public DateTime? 入出庫日時 { get; set; }
}
