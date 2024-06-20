using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_倉庫
/// </summary>
public partial class DivSoko
{
    /// <summary>
    /// 倉庫コード
    /// </summary>
    public string SokoCd { get; set; } = null;

    /// <summary>
    /// 倉庫名
    /// </summary>
    public string SokoName { get; set; } = null;
}
