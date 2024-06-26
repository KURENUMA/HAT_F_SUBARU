using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 雨どいデータ情報:雨どいデータ情報を格納するテーブル
/// </summary>
public partial class Amadoi
{
    /// <summary>
    /// 電化管理番号
    /// </summary>
    public string DenkaNo { get; set; }

    /// <summary>
    /// 表示順
    /// </summary>
    public string DenkaSort { get; set; }

    /// <summary>
    /// 計上ステータス,0:未計上/1:計上済
    /// </summary>
    public short? AppropState { get; set; }

    /// <summary>
    /// 顧客品名
    /// </summary>
    public string Hinmei { get; set; }

    /// <summary>
    /// 顧客規格
    /// </summary>
    public string Kikaku { get; set; }

    /// <summary>
    /// 品目テキスト
    /// </summary>
    public string HinmokuName { get; set; }

    /// <summary>
    /// 品目CD
    /// </summary>
    public string HinmokuCd { get; set; }

    /// <summary>
    /// 色R3コード:マッピングなし
    /// </summary>
    public string ColorR3code { get; set; }

    /// <summary>
    /// 顧客色表記:マッピングなし
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// 色呼称:マッピングなし
    /// </summary>
    public string ColorName { get; set; }

    /// <summary>
    /// 基本数量
    /// </summary>
    public int? KihonSuryo { get; set; }

    /// <summary>
    /// 補正数量:マッピングなし
    /// </summary>
    public int? HoseiSuryo { get; set; }

    /// <summary>
    /// 単位
    /// </summary>
    public string Tani { get; set; }

    /// <summary>
    /// 無償扱い単価:マッピングなし
    /// </summary>
    public decimal? MushoTanka { get; set; }

    /// <summary>
    /// 有償扱い単価:マッピングなし
    /// </summary>
    public decimal? YushoTanka { get; set; }

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
