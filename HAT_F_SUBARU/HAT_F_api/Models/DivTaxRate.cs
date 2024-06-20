using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 税率区分
/// </summary>
public partial class DivTaxRate
{
    /// <summary>
    /// 税率区分CD
    /// </summary>
    public string TaxRateCd { get; set; }

    /// <summary>
    /// 税率区分名
    /// </summary>
    public string TaxRateName { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    public short TaxRate { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
