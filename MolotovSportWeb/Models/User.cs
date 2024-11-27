using System;
using System.Collections.Generic;

namespace MolotovSportWeb.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<ShopingCart> ShopingCarts { get; set; } = new List<ShopingCart>();
}
