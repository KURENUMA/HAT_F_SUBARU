using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewPurchaseBillingDetail
{
    public string 照合ステータス { get; set; }

    public string SaveKey { get; set; }

    public string DenSort { get; set; }

    public string DenNoLine { get; set; }

    public string 伝票番号 { get; set; }

    public string 伝区 { get; set; }

    public string F仕入先コード { get; set; }

    public string F仕入先 { get; set; }

    public DateTime? F納日 { get; set; }

    public string F注文番号 { get; set; }

    public string F子番 { get; set; }

    public string F注番 { get; set; }

    public string F倉庫コード { get; set; }

    public short? 売上確定 { get; set; }

    public string F商品コード { get; set; }

    public string F商品名 { get; set; }

    public int? F数量 { get; set; }

    public decimal? F単価 { get; set; }

    public decimal? F金額 { get; set; }

    public short? F消費税率 { get; set; }

    public string F消費税区分 { get; set; }

    public int? F受発注者 { get; set; }

    public string F受発注者部門コード { get; set; }

    public string 仕入番号 { get; set; }

    public short? 仕入行番号 { get; set; }

    public string M仕入先コード { get; set; }

    public short? M仕入先コード枝番 { get; set; }

    public DateTime? M納入日 { get; set; }

    public string M注文番号 { get; set; }

    public string M注番 { get; set; }

    public string M納品書番号 { get; set; }

    public string M商品コード { get; set; }

    public string M商品名 { get; set; }

    public decimal? M単価 { get; set; }

    public decimal? M数量 { get; set; }

    public decimal? M金額 { get; set; }

    public short? M消費税率 { get; set; }

    public string M消費税区分 { get; set; }

    public string M区分 { get; set; }

    public string M伝票番号 { get; set; }

    public string M伝区 { get; set; }
}
