using System;
using System.Collections.Generic;

namespace CafeDirect.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public long Employee { get; set; }

    public string WorkMode { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual Employee EmployeeNavigation { get; set; } = null!;
}
