using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_従業員
/// </summary>
public partial class DivEmployee
{
    /// <summary>
    /// 従業員コード
    /// </summary>
    public string EmployeeCd { get; set; } = null;

    /// <summary>
    /// 従業員名
    /// </summary>
    public string EmployeeName { get; set; } = null;
}
