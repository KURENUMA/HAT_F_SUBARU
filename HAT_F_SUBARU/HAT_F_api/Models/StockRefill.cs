using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫補充★
/// </summary>
public partial class StockRefill
{
    /// <summary>
    /// 倉庫コード
    /// </summary>
    public string WhCode { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 在庫閾値,在庫数が下回ったら発注候補にします
    /// </summary>
    public short StockThreshold { get; set; }

    /// <summary>
    /// 発注個数
    /// </summary>
    public short OrderQuantity { get; set; }

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
