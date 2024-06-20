using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// H注番の連番管理
/// </summary>
public partial class HatOrderNoSequence
{
    /// <summary>
    /// H注番のキー
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// 連番(最大999)
    /// </summary>
    public int Number { get; set; }
}
