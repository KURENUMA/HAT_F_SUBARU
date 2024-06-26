﻿using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 契約単価・販売
/// </summary>
public partial class KTankaSale1
{
    /// <summary>
    /// 商品コード
    /// </summary>
    public string ProdCode { get; set; }

    /// <summary>
    /// 顧客コード(6桁)
    /// </summary>
    public string CustCode { get; set; }

    /// <summary>
    /// 記号
    /// </summary>
    public string Sign { get; set; }

    /// <summary>
    /// 掛率
    /// </summary>
    public decimal? Rate { get; set; }

    /// <summary>
    /// 開始日
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 承認済
    /// </summary>
    public bool? Approved { get; set; }

    /// <summary>
    /// 変更申請ID
    /// </summary>
    public string ChangeApplicationId { get; set; }

    /// <summary>
    /// 作成日時
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 作成者名
    /// </summary>
    public int? Creator { get; set; }

    /// <summary>
    /// 更新日時
    /// </summary>
    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// 更新者名
    /// </summary>
    public int? Updater { get; set; }
}
