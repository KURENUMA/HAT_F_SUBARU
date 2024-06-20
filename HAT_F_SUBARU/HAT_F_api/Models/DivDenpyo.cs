using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_伝票
/// </summary>
public partial class DivDenpyo
{
    /// <summary>
    /// 伝票コード
    /// </summary>
    public string DenpyoCd { get; set; } = null;

    /// <summary>
    /// 伝票名
    /// </summary>
    public string DenpyoName { get; set; } = null;
}
