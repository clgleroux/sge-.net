using System;
using System.Collections.Generic;

namespace backend.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
