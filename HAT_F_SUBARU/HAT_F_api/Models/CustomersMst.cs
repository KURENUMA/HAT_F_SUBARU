using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 顧客マスタ
/// </summary>
public partial class CustomersMst
{
    /// <summary>
    /// 顧客コード
    /// </summary>
    public string CustCode { get; set; }

    /// <summary>
    /// 顧客区分
    /// </summary>
    public short? CustType { get; set; }

    /// <summary>
    /// 請求先コード
    /// </summary>
    public string ArCode { get; set; }

    /// <summary>
    /// 請求先枝番
    /// </summary>
    public short? ArSubNo { get; set; }

    /// <summary>
    /// 回収先コード
    /// </summary>
    public string PayerCode { get; set; }

    /// <summary>
    /// 回収先枝番
    /// </summary>
    public short? PayerSubNo { get; set; }

    /// <summary>
    /// 顧客名
    /// </summary>
    public string CustName { get; set; }

    /// <summary>
    /// 顧客名カナ
    /// </summary>
    public string CustKana { get; set; }

    /// <summary>
    /// 自社担当者コード
    /// </summary>
    public string EmpCode { get; set; }

    /// <summary>
    /// 顧客担当者名
    /// </summary>
    public string CustUserName { get; set; }

    /// <summary>
    /// 顧客部門名
    /// </summary>
    public string CustUserDepName { get; set; }

    /// <summary>
    /// 顧客郵便番号
    /// </summary>
    public string CustZipCode { get; set; }

    /// <summary>
    /// 顧客都道府県
    /// </summary>
    public string CustState { get; set; }

    /// <summary>
    /// 顧客住所１
    /// </summary>
    public string CustAddress1 { get; set; }

    /// <summary>
    /// 顧客住所２
    /// </summary>
    public string CustAddress2 { get; set; }

    public string CustAddress3 { get; set; }

    /// <summary>
    /// 顧客電話番号
    /// </summary>
    public string CustTel { get; set; }

    /// <summary>
    /// 顧客FAX番号
    /// </summary>
    public string CustFax { get; set; }

    /// <summary>
    /// 顧客メールアドレス
    /// </summary>
    public string CustEmail { get; set; }

    /// <summary>
    /// 顧客請求区分,1:都度請求,2:締請求
    /// </summary>
    public short? CustArFlag { get; set; }

    /// <summary>
    /// 顧客締日,15:15日締め
    /// </summary>
    public short? CustCloseDate { get; set; }

    /// <summary>
    /// サイト_現金_日数★
    /// </summary>
    public short? SiteDaysCash { get; set; }

    /// <summary>
    /// サイト_手形_日数★
    /// </summary>
    public short? SiteDaysBill { get; set; }

    /// <summary>
    /// 金種_現金_割合★
    /// </summary>
    public decimal? DenomRateCash { get; set; }

    /// <summary>
    /// 金種_自振手形_割合
    /// </summary>
    public decimal? DenomRateBillAuto { get; set; }

    /// <summary>
    /// 金種_転譲手形_割合
    /// </summary>
    public decimal? DenomRateBillTransfer { get; set; }

    /// <summary>
    /// 請求締日★
    /// </summary>
    public short? ClaimCloseDay { get; set; }

    /// <summary>
    /// 集日数(締日から集金日まで日数)★
    /// </summary>
    public short? CloseToCollectionDays { get; set; }

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
