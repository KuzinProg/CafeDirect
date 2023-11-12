using System;
using System.Collections.Generic;

namespace CafeDirect.Models;

public partial class Menu
{
    public long MenuId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
