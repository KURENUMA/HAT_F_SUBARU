using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入請求データ:仕入請求取込された情報を格納
/// </summary>
public partial class PuBilling
{
    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string PuNo { get; set; }

    /// <summary>
    /// 仕入支払年月
    /// </summary>
    public DateTime? PuPayYearMonth { get; set; }

    /// <summary>
    /// 仕入先伝票番号
    /// </summary>
    public string PuDenNo { get; set; }

    /// <summary>
    /// 仕入日（納入日）
    /// </summary>
    public DateTime? PuDate { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 商品名
    /// </summary>
    public string ProdName { get; set; }

    /// <summary>
    /// 仕入単価（M単価）
    /// </summary>
    public decimal? PuPrice { get; set; }

    /// <summary>
    /// 仕入数量（M数量）
    /// </summary>
    public decimal PuQuantity { get; set; }

    /// <summary>
    /// 消費税率
    /// </summary>
    public int? PuTax { get; set; }

    /// <summary>
    /// 発注行番号:受注詳細.CHUBAN
    /// </summary>
    public string PoRowNo { get; set; }

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
