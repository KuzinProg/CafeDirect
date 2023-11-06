using System;
using System.Collections.Generic;

namespace CafeDirect.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int Order { get; set; }

    public int? MenuItem { get; set; }

    public virtual Menu? MenuItemNavigation { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;
}
