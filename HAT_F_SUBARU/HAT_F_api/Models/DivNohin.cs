using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_納品
/// </summary>
public partial class DivNohin
{
    /// <summary>
    /// 納品コード
    /// </summary>
    public string NohinCd { get; set; } = null;

    /// <summary>
    /// 納品名
    /// </summary>
    public string NohinName { get; set; } = null;
}
