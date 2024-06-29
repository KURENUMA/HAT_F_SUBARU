using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 顧客担当
/// </summary>
public partial class CustomersCharger
{
    /// <summary>
    /// 年度
    /// </summary>
    public int? ChargeYear { get; set; }

    /// <summary>
    /// 顧客コード
    /// </summary>
    public string CustCode { get; set; }

    /// <summary>
    /// チームコード
    /// </summary>
    public string TeamCd { get; set; }

    /// <summary>
    /// 社員ID
    /// </summary>
    public int? EmpId { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
