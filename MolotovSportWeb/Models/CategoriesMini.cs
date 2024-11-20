using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class CategoriesMini
{
    public int CategoryMiniId { get; set; }

    public int CategoryId { get; set; }

    public string CategoryMiniName { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
