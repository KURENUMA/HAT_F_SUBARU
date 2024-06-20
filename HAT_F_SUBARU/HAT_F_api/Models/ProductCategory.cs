using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 商品分類マスタ
/// </summary>
public partial class ProductCategory
{
    /// <summary>
    /// 商品分類コード
    /// </summary>
    public string CategoryCode { get; set; }

    /// <summary>
    /// 商品分類名
    /// </summary>
    public string ProdCateName { get; set; }

    /// <summary>
    /// 商品分類階層
    /// </summary>
    public short? CategoryLayer { get; set; }

    /// <summary>
    /// 商品分類パス
    /// </summary>
    public string CategoryPath { get; set; }

    /// <summary>
    /// 最下層区分
    /// </summary>
    public short? LowestFlug { get; set; }

    public string Cd32 { get; set; }

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
