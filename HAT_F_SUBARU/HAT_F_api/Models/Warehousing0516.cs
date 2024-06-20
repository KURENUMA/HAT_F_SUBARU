using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 入出庫★
/// </summary>
public partial class Warehousing0516
{
    /// <summary>
    /// 入出庫ID,自動採番
    /// </summary>
    public long WarehousingId { get; set; }

    /// <summary>
    /// 入出庫区分,I:入庫 O:出庫
    /// </summary>
    public string WarehousingDiv { get; set; }

    /// <summary>
    /// 入出庫日時
    /// </summary>
    public DateTime? WarehousingDatetime { get; set; }

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
    /// 良品区分,G:良品 F:不良品 U:未検品
    /// </summary>
    public string QualityType { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// 数量 (バラ)
    /// </summary>
    public int? QuantityBara { get; set; }

    /// <summary>
    /// 入出庫理由,0:通常 1:返品 2:検品による数量調整 3:棚卸し数量調整
    /// </summary>
    public short? WarehousingCause { get; set; }

    /// <summary>
    /// 受注詳細のCHUBAN,(H注番+連番 マルマ入庫入力用)
    /// </summary>
    public string Chuban { get; set; }

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
