using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫引当★
/// </summary>
public partial class StockReserve
{
    /// <summary>
    /// 在庫引当ID,自動採番
    /// </summary>
    public long StockReserveId { get; set; }

    /// <summary>
    /// 在庫引当日時
    /// </summary>
    public DateTime? StockReserveDatetime { get; set; }

    /// <summary>
    /// 伝票番号
    /// </summary>
    public string DenNo { get; set; }

    /// <summary>
    /// 倉庫コード
    /// </summary>
    public string WhCode { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 在庫区分,1:自社在庫 2:預り在庫 (マルマ)
    /// </summary>
    public string StockType { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// 数量 (バラ)
    /// </summary>
    public int? QuantityBara { get; set; }

    /// <summary>
    /// 入出庫理由,0:通常 1:棚卸し調整
    /// </summary>
    public short? WarehousingCause { get; set; }

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
