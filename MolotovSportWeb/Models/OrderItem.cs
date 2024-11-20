using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class OrderItem
{
    public int OrderItemsId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int SizeId { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ProductSize Size { get; set; } = null!;
}
