using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

/// <summary>
/// ユーザー割当役割
/// </summary>
public partial class UserAssignedRole
{
    /// <summary>
    /// 社員ID
    /// </summary>
    public int EmpId { get; set; }

    /// <summary>
    /// 役割コード
    /// </summary>
    public string UserRoleCd { get; set; }
}
