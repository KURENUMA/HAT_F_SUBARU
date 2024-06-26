using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 倉庫マスタ
/// </summary>
public partial class WhMst
{
    /// <summary>
    /// 倉庫コード
    /// </summary>
    public string WhCode { get; set; }

    /// <summary>
    /// 倉庫名
    /// </summary>
    public string WhName { get; set; }

    /// <summary>
    /// 倉庫区分,N:通常倉庫(HAT-F) S:仕入先(マルマ) 不使用⇒C:得意先 D:部門倉庫 P:製品倉庫 M:原材料倉庫
    /// </summary>
    public string WhType { get; set; }

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

    /// <summary>
    /// 住所３
    /// </summary>
    public string Address3 { get; set; }

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
