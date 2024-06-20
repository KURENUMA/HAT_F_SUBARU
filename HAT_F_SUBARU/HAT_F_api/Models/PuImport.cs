using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入取込データ★
/// </summary>
public partial class PuImport
{
    /// <summary>
    /// 仕入取込番号
    /// </summary>
    public string PuImportNo { get; set; }

    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string SupCode { get; set; }

    /// <summary>
    /// 仕入先枝番
    /// </summary>
    public short? SupSubNo { get; set; }

    /// <summary>
    /// 支払先コード
    /// </summary>
    public string PaySupCode { get; set; }

    /// <summary>
    /// 納品書番号
    /// </summary>
    public string DeliveryNo { get; set; }

    /// <summary>
    /// 納入日
    /// </summary>
    public DateTime Noubi { get; set; }

    /// <summary>
    /// HAT-F注文番号
    /// </summary>
    public string HatOrderNo { get; set; }

    /// <summary>
    /// 子番
    /// </summary>
    public short? Koban { get; set; }

    /// <summary>
    /// F注番
    /// </summary>
    public string Chuban { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 商品名
    /// </summary>
    public string ProdName { get; set; }

    /// <summary>
    /// 仕入数量
    /// </summary>
    public short PuQuantity { get; set; }

    /// <summary>
    /// 仕入単価
    /// </summary>
    public decimal? PoPrice { get; set; }

    /// <summary>
    /// 消費税
    /// </summary>
    public string TaxFlg { get; set; }

    /// <summary>
    /// 消費税率
    /// </summary>
    public short? TaxRate { get; set; }

    /// <summary>
    /// 区分:0orNULL:売上
    /// </summary>
    public string PuKbn { get; set; }

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
