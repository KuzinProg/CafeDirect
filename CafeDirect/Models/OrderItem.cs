﻿using System;
using System.Collections.Generic;

namespace CafeDirect.Models;

public partial class OrderItem
{
    public long OrderItemId { get; set; }

    public long Order { get; set; }

    public long? MenuItem { get; set; }

    public virtual Menu? MenuItemNavigation { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;
}
