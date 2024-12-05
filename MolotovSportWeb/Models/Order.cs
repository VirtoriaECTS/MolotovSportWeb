using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderData { get; set; }

    public int TotalAmout { get; set; }

    public bool StatusOrder { get; set; }

    public string Adress { get; set; } = null!;

    public int PriceDeliviry { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
