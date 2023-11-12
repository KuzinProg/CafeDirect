using System;
using System.Collections.Generic;

namespace CafeDirect.Models;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public string Login { get; set; } = null!;

    public string? Password { get; set; }

    public string Role { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string Status { get; set; } = null!;

    public string? Photo { get; set; }

    public string? Contract { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
