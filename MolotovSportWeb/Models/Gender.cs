using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
