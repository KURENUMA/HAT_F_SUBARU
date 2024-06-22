using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 工事店マスタ★,主に住友林業用
/// </summary>
public partial class ConstructionShopMst
{
    /// <summary>
    /// 工事店コード(KOJICD):KOJICD 13桁 + 予備
    /// </summary>
    public string ConstCode { get; set; }

    /// <summary>
    /// 工事店区分
    /// </summary>
    public short? ConstType { get; set; }

    /// <summary>
    /// 工事店名
    /// </summary>
    public string ConstName { get; set; }

    /// <summary>
    /// 工事店名カナ
    /// </summary>
    public string ConstKana { get; set; }

    public string CustCode { get; set; }

    /// <summary>
    /// 工事店郵便番号
    /// </summary>
    public string ConstZipCode { get; set; }

    /// <summary>
    /// 工事店都道府県
    /// </summary>
    public string ConstState { get; set; }

    /// <summary>
    /// 工事店住所１
    /// </summary>
    public string ConstAddress1 { get; set; }

    /// <summary>
    /// 工事店住所２
    /// </summary>
    public string ConstAddress2 { get; set; }

    /// <summary>
    /// 工事店住所３
    /// </summary>
    public string ConstAddress3 { get; set; }

    /// <summary>
    /// 工事店電話番号
    /// </summary>
    public string ConstTel { get; set; }

    /// <summary>
    /// 工事店FAX番号
    /// </summary>
    public string ConstFax { get; set; }

    /// <summary>
    /// 工事店メールアドレス
    /// </summary>
    public string ConstEmail { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool? Deleted { get; set; }

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
