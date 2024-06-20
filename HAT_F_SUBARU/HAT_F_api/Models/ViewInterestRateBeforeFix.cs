using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewInterestRateBeforeFix
{
    public string 伝票番号 { get; set; }

    public string 伝票区分 { get; set; }

    public string 伝票区分名 { get; set; }

    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public long? 売上合計金額 { get; set; }

    public decimal? 利率 { get; set; }

    public string 売上記号 { get; set; }

    public decimal? 売上単価 { get; set; }

    public int? 数量 { get; set; }

    public decimal? 売上額 { get; set; }

    public decimal? 売上掛率 { get; set; }

    public string 仕入記号 { get; set; }

    public decimal? 仕入単価 { get; set; }

    public decimal? 仕入額 { get; set; }

    public decimal? 仕入掛率 { get; set; }

    public decimal? 定価 { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public string 営業担当者名 { get; set; }

    public string コメント者 { get; set; }

    public string コメント役職 { get; set; }

    public string コメント { get; set; }

    public string SaveKey { get; set; }

    public string DenSort { get; set; }

    public string DenNoLine { get; set; }
}
