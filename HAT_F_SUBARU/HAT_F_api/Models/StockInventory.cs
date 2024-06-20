using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫データ_棚卸★
/// </summary>
public partial class StockInventory
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
    /// ロット番号,★削除予定
    /// </summary>
    public string RotNo { get; set; }

    /// <summary>
    /// 在庫区分,1:自社在庫 2:預り在庫 (マルマ)
    /// </summary>
    public string StockType { get; set; }

    /// <summary>
    /// 良品区分,G:良品 F:不良品 U:未検品
    /// </summary>
    public string QualityType { get; set; }

    /// <summary>
    /// 棚卸年月,yyyyMM01で保存
    /// </summary>
    public DateTime InventoryYearmonth { get; set; }

    /// <summary>
    /// 棚卸NO,倉庫+区分で連番
    /// </summary>
    public int? StockNo { get; set; }

    /// <summary>
    /// ランク
    /// </summary>
    public string StockRank { get; set; }

    /// <summary>
    /// 実在庫数
    /// </summary>
    public short? Actual { get; set; }

    /// <summary>
    /// 実在庫数 (バラ)
    /// </summary>
    public short? ActualBara { get; set; }

    /// <summary>
    /// 管理在庫数
    /// </summary>
    public short? Managed { get; set; }

    /// <summary>
    /// 管理在庫数 (バラ)
    /// </summary>
    public short? ManagedBara { get; set; }

    /// <summary>
    /// 状況,「連携済」等の対応コード
    /// </summary>
    public string Status { get; set; }

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
