using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class UserAuthentication
{
    public int EmpId { get; set; }

    public string LoginPassword { get; set; }
}
