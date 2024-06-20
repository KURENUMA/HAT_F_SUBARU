using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewWarehousingShipping
{
    public string SaveKey { get; set; }

    public string DenSort { get; set; }

    public string 伝区 { get; set; }

    public string 倉庫ステータス { get; set; }

    public string 伝票番号 { get; set; }

    public string 取引先 { get; set; }

    public string 配送先 { get; set; }

    public string 配送先住所 { get; set; }

    public string 配送パターン { get; set; }

    public string 出荷指示書印刷 { get; set; }

    public DateTime? 入荷予定 { get; set; }

    public DateTime? 入荷日 { get; set; }

    public DateTime? 納期 { get; set; }

    public DateTime? 出荷日 { get; set; }

    public DateTime? 到着予定日 { get; set; }
}
