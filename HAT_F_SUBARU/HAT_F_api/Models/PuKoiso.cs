using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入データ
/// </summary>
public partial class PuKoiso
{
    /// <summary>
    /// 仕入番号
    /// </summary>
    public string PuNo { get; set; }

    /// <summary>
    /// 仕入日
    /// </summary>
    public DateTime? PuDate { get; set; }

    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string SupCode { get; set; }

    /// <summary>
    /// 仕入先枝番
    /// </summary>
    public short? SupSubNo { get; set; }

    /// <summary>
    /// 支払先コード★
    /// </summary>
    public string PaySupCode { get; set; }

    /// <summary>
    /// 仕入担当者ID（社員ID）★
    /// </summary>
    public int EmpId { get; set; }

    /// <summary>
    /// 開始日
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 部門コード
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 仕入金額合計（M金額）
    /// </summary>
    public decimal? PuAmmount { get; set; }

    /// <summary>
    /// 消費税金額
    /// </summary>
    public decimal CmpTax { get; set; }

    /// <summary>
    /// HAT-F注文番号★
    /// </summary>
    public string HatOrderNo { get; set; }

    /// <summary>
    /// 備考
    /// </summary>
    public string SlipComment { get; set; }

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
