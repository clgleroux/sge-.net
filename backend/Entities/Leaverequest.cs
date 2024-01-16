using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Leaverequest
{
    public int LeaveRequestId { get; set; }

    public int? EmployeeId { get; set; }

    public int? StatusId { get; set; }

    public DateOnly RequestDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Status? Status { get; set; }
}
