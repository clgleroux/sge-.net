using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Status
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Leaverequest> Leaverequests { get; set; } = new List<Leaverequest>();
}
