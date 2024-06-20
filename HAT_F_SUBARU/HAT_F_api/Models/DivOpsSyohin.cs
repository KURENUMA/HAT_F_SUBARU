using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// DIV_OPS商品
/// </summary>
public partial class DivOpsSyohin
{
    /// <summary>
    /// OPS商品コード
    /// </summary>
    public string OpsSyohinCd { get; set; } = null;

    /// <summary>
    /// OPS商品名
    /// </summary>
    public string OpsSyohinName { get; set; } = null;
}
