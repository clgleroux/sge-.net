using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual Employee? Employee { get; set; }
}
