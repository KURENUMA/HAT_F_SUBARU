using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 売上確定前利率異常チェック
/// </summary>
public partial class InterestRateCheckFixed
{
    /// <summary>
    /// 利率異常チェックID
    /// </summary>
    public int InterestRateCheckFixedId { get; set; }

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
    /// 売上番号
    /// </summary>
    public string SalesNo { get; set; }

    /// <summary>
    /// 売上行番号
    /// </summary>
    public short? RowNo { get; set; }

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
