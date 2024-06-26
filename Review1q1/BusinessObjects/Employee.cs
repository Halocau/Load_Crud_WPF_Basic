using System;
using System.Collections.Generic;

namespace Review1q1.BusinessObjects;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public string? Idnumber { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
