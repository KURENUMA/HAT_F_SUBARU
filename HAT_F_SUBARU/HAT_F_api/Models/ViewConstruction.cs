using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewConstruction
{
    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 検索キー { get; set; }

    public short? 受注状態 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先 { get; set; }

    public string 登録者 { get; set; }

    public string 現場郵便番号 { get; set; }

    public string 現場住所1 { get; set; }

    public string 現場住所2 { get; set; }

    public string 現場住所3 { get; set; }

    public DateTime? 引合日 { get; set; }

    public string 建設会社名 { get; set; }

    public string 備考 { get; set; }
}
