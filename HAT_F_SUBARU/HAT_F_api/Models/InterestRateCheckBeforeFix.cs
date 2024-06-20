using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 売上確定前利率異常チェック
/// </summary>
public partial class InterestRateCheckBeforeFix
{
    /// <summary>
    /// 利率異常チェックID
    /// </summary>
    public int InterestRateCheckBeforeFixId { get; set; }

    /// <summary>
    /// チェック日時
    /// </summary>
    public DateTime? CheckDatetime { get; set; }

    /// <summary>
    /// チェック者
    /// </summary>
    public int? CheckerId { get; set; }

    /// <summary>
    /// チェック者役職
    /// </summary>
    public string CheckerPost { get; set; }

    /// <summary>
    /// SAVE_KEY
    /// </summary>
    public string SaveKey { get; set; }

    /// <summary>
    /// DEN_SORT
    /// </summary>
    public string DenSort { get; set; }

    /// <summary>
    /// DEN_NO_LINE
    /// </summary>
    public string DenNoLine { get; set; }

    /// <summary>
    /// コメント
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
