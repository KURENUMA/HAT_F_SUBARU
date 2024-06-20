using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 受注詳細
/// </summary>
public partial class FosJyuchuD
{
    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string SaveKey { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderNoLine { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderState { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string DenNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string DenSort { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string DenNoLine { get; set; }

    /// <summary>
    /// 子番
    /// </summary>
    public short? Koban { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string EstimateNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string EstCoNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public int? LineNo { get; set; }

    /// <summary>
    /// 分類
    /// </summary>
    public string SyobunCd { get; set; }

    /// <summary>
    /// 商品コード・名称
    /// </summary>
    public string SyohinCd { get; set; }

    /// <summary>
    /// 商品コード・名称
    /// </summary>
    public string SyohinName { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Code5 { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Kikaku { get; set; }

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
    /// 《画面対応なし》
    /// </summary>
    public string TeiKigou { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public decimal? TeiKake { get; set; }

    /// <summary>
    /// 定価単価
    /// </summary>
    public decimal? TeiTan { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public decimal? TeiKin { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Uauto { get; set; }

    /// <summary>
    /// 売上記号
    /// </summary>
    public string UriKigou { get; set; }

    /// <summary>
    /// 掛率(売上)
    /// </summary>
    public decimal? UriKake { get; set; }

    /// <summary>
    /// 売上単価
    /// </summary>
    public decimal? UriTan { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public decimal? UriKin { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string UriType { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Gauto { get; set; }

    /// <summary>
    /// 仕入記号
    /// </summary>
    public string SiiKigou { get; set; }

    /// <summary>
    /// 掛率(仕入)
    /// </summary>
    public decimal? SiiKake { get; set; }

    /// <summary>
    /// 仕入単価
    /// </summary>
    public decimal? SiiTan { get; set; }

    /// <summary>
    /// 回答単価
    /// </summary>
    public decimal? SiiAnswTan { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public decimal? SiiKin { get; set; }

    /// <summary>
    /// 納日
    /// </summary>
    public DateTime? Nouki { get; set; }

    /// <summary>
    /// 消費税
    /// </summary>
    public string TaxFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public int? Dencd { get; set; }

    /// <summary>
    /// 売区
    /// </summary>
    public string Urikubn { get; set; }

    /// <summary>
    /// 倉庫
    /// </summary>
    public string SokoCd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Chuban { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? ReqNouki { get; set; }

    /// <summary>
    /// 仕入
    /// </summary>
    public string ShiresakiCd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Sbiko { get; set; }

    /// <summary>
    /// 行備考
    /// </summary>
    public string Lbiko { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Locat { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderDenNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OrderDenLineNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string DelFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string MoveFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? InpDate { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Dseq { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Hinban { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string AddDetailFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OyahinFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string Oyahinb { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string HopeOrderNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string HopeMeisaiNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string EcoFlg { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsOrderNo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? OpsRecYmd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsLineno { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsSokocd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? OpsShukkadt { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public DateTime? OpsNyukabi { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public int? OpsKonpo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsTani { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public int? OpsBara { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public decimal? OpsUtanka { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsUauto { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsUkigo { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public decimal? OpsUritu { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsSyohinCd { get; set; }

    /// <summary>
    /// 《画面対応なし》
    /// </summary>
    public string OpsKikaku { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.受注番号》
    /// </summary>
    public string GOrderNo { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.引当数量》
    /// </summary>
    public short? GReserveQty { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.出荷指示数量》
    /// </summary>
    public short? GDeliveryOrderQty { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.出荷済数量》
    /// </summary>
    public short? GDeliveredQty { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.完了フラグ》
    /// </summary>
    public short? GCompleteFlg { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.値引金額》
    /// </summary>
    public short? GDiscount { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.作成日時》
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.作成者》
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.更新日時》
    /// </summary>
    public DateTime UpdateDate { get; set; }

    /// <summary>
    /// 《GLASS.受注データ.更新者》
    /// </summary>
    public int? Updater { get; set; }
}
