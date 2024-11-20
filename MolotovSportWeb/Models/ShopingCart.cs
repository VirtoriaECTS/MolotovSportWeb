using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class ShopingCart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public decimal TotalAmout { get; set; }

    public virtual ICollection<ShopingItem> ShopingItems { get; set; } = new List<ShopingItem>();

    public virtual User User { get; set; } = null!;
}
