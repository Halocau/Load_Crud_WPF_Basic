using System;
using System.Collections.Generic;

namespace Review1q1.BusinessObjects;

public partial class Room
{
    public string Title { get; set; } = null!;

    public int? Square { get; set; }

    public int? Floor { get; set; }

    public string? Description { get; set; }

    public string? Comment { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
