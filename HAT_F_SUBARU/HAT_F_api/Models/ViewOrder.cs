using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewOrder
{
    public string SaveKey { get; set; }

    public string DenSort { get; set; }

    public string Hat注文番号 { get; set; }

    public string 発注状態 { get; set; }

    public string 受注番号 { get; set; }

    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 伝票番号 { get; set; }

    public string 伝票区分 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先名 { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public string 受発注者 { get; set; }

    public string 入力者 { get; set; }
}
