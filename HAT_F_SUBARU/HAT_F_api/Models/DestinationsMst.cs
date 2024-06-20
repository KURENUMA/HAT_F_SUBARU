using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 出荷先マスタ
/// </summary>
public partial class DestinationsMst
{
    public string CustCode { get; set; }

    /// <summary>
    /// 顧客枝番
    /// </summary>
    public short CustSubNo { get; set; }

    /// <summary>
    /// 出荷先番号
    /// </summary>
    public short DistNo { get; set; }

    /// <summary>
    /// 出荷先名１
    /// </summary>
    public string DistName1 { get; set; }

    /// <summary>
    /// 出荷先名２
    /// </summary>
    public string DistName2 { get; set; }

    /// <summary>
    /// 地域コード
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// 出荷先郵便番号
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    /// 出荷先住所１
    /// </summary>
    public string Address1 { get; set; }

    /// <summary>
    /// 出荷先住所２
    /// </summary>
    public string Address2 { get; set; }

    /// <summary>
    /// 出荷先住所３
    /// </summary>
    public string Address3 { get; set; }

    /// <summary>
    /// 出荷先電話番号
    /// </summary>
    public string DestTel { get; set; }

    /// <summary>
    /// 出荷先電話FAX
    /// </summary>
    public string DestFax { get; set; }

    /// <summary>
    /// 取引先コード
    /// </summary>
    public string CompCode { get; set; }

    /// <summary>
    /// 工事店コード
    /// </summary>
    public string KojitenCode { get; set; }

    public string GenbaCode { get; set; }

    /// <summary>
    /// 備考
    /// </summary>
    public string Remarks { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

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
