using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class ProductSize
{
    public int SizeId { get; set; }

    public int ProductId { get; set; }

    public string Size { get; set; } = null!;

    public int Count { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ShopingItem> ShopingItems { get; set; } = new List<ShopingItem>();
}
