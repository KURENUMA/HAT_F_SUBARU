using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 仕入データ明細
/// </summary>
public partial class PuDetails0603
{
    /// <summary>
    /// 仕入番号:発注番号+仕入先コード+YYYYMM
    /// </summary>
    public string PuNo { get; set; }

    /// <summary>
    /// 仕入行番号
    /// </summary>
    public short PuRowNo { get; set; }

    /// <summary>
    /// 仕入行表示番号（M注番）
    /// </summary>
    public string PuRowDspNo { get; set; }

    /// <summary>
    /// 発注行番号:発注データ明細.PO_ROW_NO
    /// </summary>
    public string PoRowNo { get; set; }

    /// <summary>
    /// 商品コード:（取込データの値を設定？）
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 倉庫コード:倉庫入庫時に設定される（直送の場合NULLのため必須を外す）
    /// </summary>
    public string WhCode { get; set; }

    /// <summary>
    /// 商品名:（取込データの値を設定？）
    /// </summary>
    public string ProdName { get; set; }

    /// <summary>
    /// 仕入単価（M単価）
    /// </summary>
    public decimal? PoPrice { get; set; }

    /// <summary>
    /// 仕入数量（M数量）
    /// </summary>
    public short PuQuantity { get; set; }

    /// <summary>
    /// 照合ステータス(0:未確認/1:編集中/2:確認済/3:未決/4:違算/5:未請求)
    /// </summary>
    public short? CheckStatus { get; set; }

    /// <summary>
    /// 仕入支払年月
    /// </summary>
    public DateTime? PuPayYearMonth { get; set; }

    public string Chuban { get; set; }

    /// <summary>
    /// 仕入訂正区分
    /// </summary>
    public string PuCorrectionType { get; set; }

    /// <summary>
    /// 元仕入番号
    /// </summary>
    public string OriginalPuNo { get; set; }

    /// <summary>
    /// 元仕入行番号
    /// </summary>
    public short? OriginalPuRowNo { get; set; }

    /// <summary>
    /// 承認対象ID
    /// </summary>
    public string ApprovalTargetId { get; set; }

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
