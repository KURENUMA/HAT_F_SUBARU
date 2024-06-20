using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class StockHistory
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
    /// ロット番号
    /// </summary>
    public string RotNo { get; set; }

    /// <summary>
    /// 在庫区分,1:自社在庫 2:預り在庫
    /// </summary>
    public string StockType { get; set; }

    /// <summary>
    /// 良品区分,G:良品 F:不良品 U:未検品
    /// </summary>
    public string QualityType { get; set; }

    /// <summary>
    /// ランク
    /// </summary>
    public string StockRank { get; set; }

    /// <summary>
    /// 実在庫数
    /// </summary>
    public short Actual { get; set; }

    public short ActualBara { get; set; }

    /// <summary>
    /// 有効在庫数
    /// </summary>
    public short Valid { get; set; }

    public short ValidBara { get; set; }

    /// <summary>
    /// 最終出荷日
    /// </summary>
    public DateTime LastDeliveryDate { get; set; }

    public bool? BeginningPeriod { get; set; }

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
