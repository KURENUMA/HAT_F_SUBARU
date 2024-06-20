using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 納品区分
/// </summary>
public partial class DivDelivery
{
    /// <summary>
    /// 納品区分CD
    /// </summary>
    public string DeliveryCd { get; set; }

    /// <summary>
    /// 納品区分名
    /// </summary>
    public string DeliveryName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
