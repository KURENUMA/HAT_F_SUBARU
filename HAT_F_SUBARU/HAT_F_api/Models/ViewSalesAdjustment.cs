using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ViewSalesAdjustment
{
    public string 売上調整番号 { get; set; }

    public string 得意先コード { get; set; }

    public string 得意先名 { get; set; }

    public DateTime? 請求年月 { get; set; }

    public short? 区分 { get; set; }

    public string 勘定科目 { get; set; }

    public string 摘要 { get; set; }

    public decimal? 金額 { get; set; }

    public string 消費税 { get; set; }

    public short? 消費税率 { get; set; }

    public string 承認要求番号 { get; set; }

    public int? 承認状態 { get; set; }

    public string 申請者 { get; set; }

    public string 承認者 { get; set; }

    public string 最終承認者 { get; set; }
}
