using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class DivAuth
{
    public int AuthId { get; set; }

    public bool AuthDeleted { get; set; }

    public string AuthCd { get; set; }

    public string AuthName { get; set; }

    public string AuthPassword { get; set; }
}
