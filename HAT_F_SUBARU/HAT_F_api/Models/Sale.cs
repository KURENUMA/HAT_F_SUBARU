using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 売上データ:倉出/マルマ:出荷指示時に登録
/// 直送:仕入請求金額照合時に登録
/// 赤黒訂正時に登録
/// </summary>
public partial class Sale
{
    /// <summary>
    /// 売上番号
    /// </summary>
    public string SalesNo { get; set; }

    /// <summary>
    /// 物件コード
    /// </summary>
    public string ConstructionCode { get; set; }

    /// <summary>
    /// 受注番号:受注ヘッダ.ORDER_NO
    /// </summary>
    public string OrderNo { get; set; }

    /// <summary>
    /// 売上日,出荷日:(倉出/マルマ:到着予定日,直送:納入日)
    /// </summary>
    public DateTime? SalesDate { get; set; }

    /// <summary>
    /// 売上区分,1:売上 2:売上返品
    /// </summary>
    public short? SalesType { get; set; }

    /// <summary>
    /// 部門コード
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 部門開始日
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 取引先コード
    /// </summary>
    public string CompCode { get; set; }

    /// <summary>
    /// 社員ID:（追加項目）
    /// </summary>
    public int EmpId { get; set; }

    /// <summary>
    /// 売上金額合計
    /// </summary>
    public decimal SalesAmnt { get; set; }

    /// <summary>
    /// 消費税合計
    /// </summary>
    public decimal CmpTax { get; set; }

    /// <summary>
    /// 備考
    /// </summary>
    public string SlipComment { get; set; }

    /// <summary>
    /// 赤黒伝票番号
    /// </summary>
    public long? UpdatedNo { get; set; }

    /// <summary>
    /// 元伝票番号
    /// </summary>
    public string OrgnlNo { get; set; }

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
