﻿using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// 便区分
/// </summary>
public partial class DivInvoiceIssue
{
    /// <summary>
    /// 便区分CD
    /// </summary>
    public string InvoiceIssueCd { get; set; }

    /// <summary>
    /// 便区分名
    /// </summary>
    public string InvoiceIssueName { get; set; }

    /// <summary>
    /// 削除済
    /// </summary>
    public bool Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
