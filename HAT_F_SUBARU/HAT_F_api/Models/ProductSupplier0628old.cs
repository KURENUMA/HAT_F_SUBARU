using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class ProductSupplier0628old
{
    /// <summary>
    /// 商品分類コード(CODE5)
    /// </summary>
    public string CategoryCode { get; set; }

    /// <summary>
    /// 仕入先コード
    /// </summary>
    public string SupCode { get; set; }

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

    public int? Updater { get; set; }
}
