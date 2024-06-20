using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_仕入先
/// </summary>
public partial class DivShiresaki
{
    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string ShiresakiCd { get; set; } = null;

    /// <summary>
    /// 仕入先名
    /// </summary>
    public string ShiresakiName { get; set; } = null;
}
