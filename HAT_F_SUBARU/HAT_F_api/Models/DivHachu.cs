using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_発注
/// </summary>
public partial class DivHachu
{
    /// <summary>
    /// 発注コード
    /// </summary>
    public string HachuCd { get; set; } = null;

    /// <summary>
    /// 発注名
    /// </summary>
    public string HachuName { get; set; } = null;
}
