using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_工事店
/// </summary>
public partial class DivKoujiten
{
    /// <summary>
    /// 工事店コード
    /// </summary>
    public string KoujitenCd { get; set; } = null;

    /// <summary>
    /// 工事店名
    /// </summary>
    public string KoujitenName { get; set; } = null;
}
