using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 在庫商品評価
/// </summary>
public partial class StockProductEvaluation
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
    /// 在庫区分,1:自社在庫 2:預り在庫 (マルマ)
    /// </summary>
    public string StockType { get; set; }

    /// <summary>
    /// ランク,HZDE他
    /// </summary>
    public string StockRank { get; set; }

    /// <summary>
    /// ABCランク
    /// </summary>
    public string AbcRank { get; set; }

    /// <summary>
    /// 評価価格
    /// </summary>
    public decimal? EvaluationPrice { get; set; }

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
