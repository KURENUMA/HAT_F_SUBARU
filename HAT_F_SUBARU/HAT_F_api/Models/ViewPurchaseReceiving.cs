using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewPurchaseReceiving
{
    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 受注番号 { get; set; }

    public string Hat注文番号 { get; set; }

    public string 伝票番号 { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public int 発注金額合計 { get; set; }

    public DateTime? 納期 { get; set; }

    public DateTime 入力日 { get; set; }

    public string 入力者 { get; set; }

    public string 営業担当 { get; set; }

    public string 得意先名 { get; set; }

    public string 得意先営業担当 { get; set; }

    public string ステータス { get; set; }

    public short? 仕入先支払月 { get; set; }

    public short? 仕入先支払日 { get; set; }

    public short? 仕入先支払方法区分 { get; set; }
}
