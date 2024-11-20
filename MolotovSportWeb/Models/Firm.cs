using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class Firm
{
    public int FirmId { get; set; }

    public string FirmName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
