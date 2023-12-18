using System;
using System.Collections.Generic;
using CafeDirect.ViewModels;

namespace CafeDirect.Models;

public class Order
{
    public long OrderId { get; set; }

    public long Waiter { get; set; }

    public string Status { get; set; }

    public DateTime? Date { get; set; }

    public int? Place { get; set; }

    public int? ClientsCount { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Employee WaiterNavigation { get; set; } = null!;
}
