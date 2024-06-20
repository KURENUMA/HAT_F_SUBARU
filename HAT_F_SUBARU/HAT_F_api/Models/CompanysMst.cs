using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 取引先マスタ
/// </summary>
public partial class CompanysMst
{
    /// <summary>
    /// 取引先コード
    /// </summary>
    public string CompCode { get; set; }

    /// <summary>
    /// 取引先名
    /// </summary>
    public string CompName { get; set; }

    /// <summary>
    /// 取引先名カナ
    /// </summary>
    public string CompKana { get; set; }

    public string CompKanaShort { get; set; }

    public string CompBranchName { get; set; }

    /// <summary>
    /// 仕入先区分
    /// </summary>
    public short? SupType { get; set; }

    /// <summary>
    /// 郵便番号
    /// </summary>
    public string ZipCode { get; set; }

    /// <summary>
    /// 都道府県
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// 住所１
    /// </summary>
    public string Address1 { get; set; }

    /// <summary>
    /// 住所２
    /// </summary>
    public string Address2 { get; set; }

    public string Address3 { get; set; }

    public string Tel { get; set; }

    public string Fax { get; set; }

    public string Fax2 { get; set; }

    /// <summary>
    /// 取引禁止フラグ
    /// </summary>
    public short? NoSalesFlg { get; set; }

    /// <summary>
    /// 雑区分
    /// </summary>
    public short? WideUseType { get; set; }

    /// <summary>
    /// 取引先グループコード
    /// </summary>
    public string CompGroupCode { get; set; }

    /// <summary>
    /// 与信限度額
    /// </summary>
    public long? MaxCredit { get; set; }

    /// <summary>
    /// 与信一時増加枠
    /// </summary>
    public long? TempCreditUp { get; set; }

    /// <summary>
    /// インボイス登録番号
    /// </summary>
    public string InvoiceRegistNumber { get; set; }

    public DateTime? DeleteDate { get; set; }

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
