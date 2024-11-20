using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<CategoriesMini> CategoriesMinis { get; set; } = new List<CategoriesMini>();
}
