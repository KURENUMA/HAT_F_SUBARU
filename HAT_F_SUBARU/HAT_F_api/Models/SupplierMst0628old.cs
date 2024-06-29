using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入先マスタ
/// </summary>
public partial class SupplierMst0628old
{
    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string SupCode { get; set; }

    /// <summary>
    /// 仕入先名
    /// </summary>
    public string SupName { get; set; }

    /// <summary>
    /// 仕入先名カナ
    /// </summary>
    public string SupKana { get; set; }

    /// <summary>
    /// 仕入先担当者名
    /// </summary>
    public string SupEmpName { get; set; }

    /// <summary>
    /// 仕入先部門名
    /// </summary>
    public string SupDepName { get; set; }

    /// <summary>
    /// 仕入先郵便番号
    /// </summary>
    public string SupZipCode { get; set; }

    /// <summary>
    /// 仕入先都道府県
    /// </summary>
    public string SupState { get; set; }

    /// <summary>
    /// 仕入先住所１
    /// </summary>
    public string SupAddress1 { get; set; }

    /// <summary>
    /// 仕入先住所２
    /// </summary>
    public string SupAddress2 { get; set; }

    /// <summary>
    /// 仕入先電話番号
    /// </summary>
    public string SupTel { get; set; }

    /// <summary>
    /// 仕入先FAX番号
    /// </summary>
    public string SupFax { get; set; }

    /// <summary>
    /// 仕入先メールアドレス
    /// </summary>
    public string SupEmail { get; set; }

    /// <summary>
    /// 発注先種別,null/0:未設定 1:橋本本体 2:橋本本体以外
    /// </summary>
    public short? SupplierType { get; set; }

    public string PayeeCode { get; set; }

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
