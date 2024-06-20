using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_運賃
/// </summary>
public partial class DivUnchin
{
    /// <summary>
    /// 運賃コード
    /// </summary>
    public string UnchinCd { get; set; } = null;

    /// <summary>
    /// 運賃名
    /// </summary>
    public string UnchinName { get; set; } = null;
}
