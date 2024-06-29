using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 物件明細:物件明細テーブル
/// </summary>
public partial class ConstructionDetail
{
    /// <summary>
    /// 物件コード
    /// </summary>
    public string ConstructionCode { get; set; }

    /// <summary>
    /// 子番
    /// </summary>
    public int Koban { get; set; }

    /// <summary>
    /// 受注確度
    /// </summary>
    public string OrderConfidence { get; set; }

    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string ShiresakiCd { get; set; }

    /// <summary>
    /// 計上ステータス,0:未計上/1:計上済
    /// </summary>
    public short? AppropState { get; set; }

    /// <summary>
    /// 商品コード
    /// </summary>
    public string SyohinCd { get; set; }

    /// <summary>
    /// 商品名称
    /// </summary>
    public string SyohinName { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int? Suryo { get; set; }

    /// <summary>
    /// 単位
    /// </summary>
    public string Tani { get; set; }

    /// <summary>
    /// バラ数
    /// </summary>
    public int? Bara { get; set; }

    /// <summary>
    /// 定価単価
    /// </summary>
    public decimal? TeiTan { get; set; }

    /// <summary>
    /// 納日
    /// </summary>
    public DateTime? Nouki { get; set; }

    /// <summary>
    /// 掛率(売上)
    /// </summary>
    public decimal? UriKake { get; set; }

    /// <summary>
    /// 売上単価
    /// </summary>
    public decimal? UriTan { get; set; }

    /// <summary>
    /// 掛率(仕入)
    /// </summary>
    public decimal? SiiKake { get; set; }

    /// <summary>
    /// 仕入単価
    /// </summary>
    public decimal? SiiTan { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 作成者
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public int? Updater { get; set; }
}
