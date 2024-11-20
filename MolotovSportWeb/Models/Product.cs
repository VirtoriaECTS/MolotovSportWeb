using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int FirmId { get; set; }

    public int CategoryId { get; set; }

    public int CategoryIdMini { get; set; }

    public int GenderId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual CategoriesMini CategoriesMini { get; set; } = null!;

    public virtual Firm Firm { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();

    public virtual ICollection<ShopingItem> ShopingItems { get; set; } = new List<ShopingItem>();
}
