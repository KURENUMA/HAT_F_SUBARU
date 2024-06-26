using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewAmadoi
{
    public string 電化管理番号 { get; set; }

    public string 表示順 { get; set; }

    public short? 計上ステータス { get; set; }

    public string 顧客品名 { get; set; }

    public string 顧客規格 { get; set; }

    public string 品目テキスト { get; set; }

    public string 品目cd { get; set; }

    public string 色r3コード { get; set; }

    public string 顧客色表記 { get; set; }

    public string 色呼称 { get; set; }

    public int? 基本数量 { get; set; }

    public int? 補正数量 { get; set; }

    public string 単位 { get; set; }

    public decimal? 無償扱い単価 { get; set; }

    public decimal? 有償扱い単価 { get; set; }
}
