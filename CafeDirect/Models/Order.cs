using System;
using System.Collections.Generic;

namespace CafeDirect.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int Waiter { get; set; }

    public string? Status { get; set; }

    public DateTime? Date { get; set; }

    public int? Place { get; set; }

    public int? ClientsCount { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Employee WaiterNavigation { get; set; } = null!;
}
