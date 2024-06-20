using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewReadySalesBatch
{
    public string 物件コード { get; set; }

    public string 物件名 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先 { get; set; }

    public short? 発注先 { get; set; }

    public string 発注先種別名 { get; set; }

    public string 受注番号 { get; set; }

    public string Hat注文番号 { get; set; }

    public string 営業担当者名 { get; set; }

    public long? 受注合計金額 { get; set; }

    public decimal? 利率 { get; set; }

    public string 伝票番号 { get; set; }

    public string 伝票区分 { get; set; }

    public string 伝票区分名 { get; set; }

    public DateTime? 納期 { get; set; }

    public string 仕入先コード { get; set; }

    public string 仕入先名 { get; set; }

    public int? 社員id { get; set; }

    public string 部門コード { get; set; }

    public DateTime? 部門開始日 { get; set; }

    public DateTime 入荷日 { get; set; }

    public string 商品コード { get; set; }

    public string 商品名 { get; set; }

    public int? 数量 { get; set; }

    public string 売上記号 { get; set; }

    public decimal? 売上単価 { get; set; }

    public decimal? 売上額 { get; set; }

    public decimal? 売上掛率 { get; set; }

    public string 仕入記号 { get; set; }

    public decimal? 仕入単価 { get; set; }

    public decimal? 仕入額 { get; set; }

    public decimal? 仕入掛率 { get; set; }

    public decimal? 定価 { get; set; }

    public string H注番 { get; set; }

    public string 明細SaveKey { get; set; }

    public string 明細DenSort { get; set; }

    public string 明細DenNoLine { get; set; }
}
