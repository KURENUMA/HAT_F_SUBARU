using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_得意先
/// </summary>
public partial class DivTokui
{
    /// <summary>
    /// 得意先コード
    /// </summary>
    public string TokuiCd { get; set; } = null;

    /// <summary>
    /// 得意先名
    /// </summary>
    public string TokuiName { get; set; } = null;
}
