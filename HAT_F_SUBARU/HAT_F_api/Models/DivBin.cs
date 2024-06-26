using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 便区分
/// </summary>
public partial class DivBin
{
    /// <summary>
    /// 便CD
    /// </summary>
    public string BinCd { get; set; }

    /// <summary>
    /// 便名称
    /// </summary>
    public string BinName { get; set; }

    /// <summary>
    /// 便名称カナ
    /// </summary>
    public string BinNameKana { get; set; }

    /// <summary>
    /// 倉庫CD
    /// </summary>
    public string WhCd { get; set; }

    /// <summary>
    /// 届種別
    /// </summary>
    public string DeliveryType { get; set; }

    /// <summary>
    /// 配送
    /// </summary>
    public string DeliveryTime { get; set; }

    /// <summary>
    /// 便種別
    /// </summary>
    public string BinType { get; set; }

    /// <summary>
    /// 印刷便名称
    /// </summary>
    public string PrintBinName { get; set; }

    /// <summary>
    /// 印刷便名称カナ
    /// </summary>
    public string PrintBinNameKana { get; set; }

    /// <summary>
    /// 印刷届種別
    /// </summary>
    public string PrintDeliveryType { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
