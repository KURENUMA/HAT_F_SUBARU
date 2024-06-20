using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewPurchaseSalesCorrection
{
    public string Hat注文番号 { get; set; }

    public string 仕入番号 { get; set; }

    public short 仕入行番号 { get; set; }

    public string 伝票番号 { get; set; }

    public string 伝票区分 { get; set; }

    public string 仕入先コード { get; set; }

    public short? 仕入先枝番 { get; set; }

    public string 仕入先 { get; set; }

    public string 得意先コード { get; set; }

    public string 変更後得意先 { get; set; }

    public string 変更後仕入先 { get; set; }

    public short? 変更後仕入先枝番 { get; set; }

    public DateTime? H納日 { get; set; }

    public string H注番 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public string 売上番号 { get; set; }

    public short 売上行番号 { get; set; }

    public int H数量 { get; set; }

    public decimal H単価 { get; set; }

    public decimal? H金額 { get; set; }

    public int? 変更後h数量 { get; set; }

    public decimal? 変更後h単価 { get; set; }

    public int? 変更後h金額 { get; set; }

    public int? H数量差分 { get; set; }

    public int? H単価差分 { get; set; }

    public int? H金額差分 { get; set; }

    public short? M照合ステータス { get; set; }

    public short M数量 { get; set; }

    public decimal? M単価 { get; set; }

    public decimal? M金額 { get; set; }

    public short? 変更後m数量 { get; set; }

    public decimal? 変更後m単価 { get; set; }

    public int? 変更後m金額 { get; set; }

    public int? M数量差分 { get; set; }

    public int? M単価差分 { get; set; }

    public int? M金額差分 { get; set; }

    public string 承認要求番号 { get; set; }

    public string 承認対象id { get; set; }

    public short? 承認対象枝番 { get; set; }
}
