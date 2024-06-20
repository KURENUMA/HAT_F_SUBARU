using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_現場
/// </summary>
public partial class DivGenba
{
    /// <summary>
    /// 現場コード
    /// </summary>
    public string GenbaCd { get; set; } = null;

    /// <summary>
    /// 現場名
    /// </summary>
    public string GenbaName { get; set; } = null;
}
