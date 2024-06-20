using System;
using System.Collections.Generic;

namespace HAT_F_api.Models;

public partial class Announcement
{
    public int AnnouncementsId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Content { get; set; }

    public bool? Displayed { get; set; }

    public short? ImportanceLevel { get; set; }

    public bool? Deleted { get; set; }

    public DateTime CreateDate { get; set; }

    public int? Creator { get; set; }

    public DateTime UpdateDate { get; set; }

    public int? Updater { get; set; }
}
