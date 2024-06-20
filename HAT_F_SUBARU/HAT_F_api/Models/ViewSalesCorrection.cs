using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewSalesCorrection
{
    public string Hat注文番号 { get; set; }

    public string 受注番号 { get; set; }

    public string 伝票番号 { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public int 請求金額合計 { get; set; }

    public int 消費税総額 { get; set; }

    public string 営業担当者名 { get; set; }

    public string ステータス { get; set; }

    public short? 顧客支払月 { get; set; }

    public short? 顧客支払日 { get; set; }

    public short? 顧客支払方法 { get; set; }
}
