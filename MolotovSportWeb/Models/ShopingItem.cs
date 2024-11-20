using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class ShopingItem
{
    public int CardItemId { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int SizeId { get; set; }

    public int Count { get; set; }

    public decimal Price { get; set; }

    public virtual ShopingCart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ProductSize Size { get; set; } = null!;
}
