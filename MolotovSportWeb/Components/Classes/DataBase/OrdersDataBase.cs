using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Models;

namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class OrdersDataBase
    {


        public List<Order> FillOrderList()
        {
            using(var context = new MolotovSportWebContext())
            {
                var orders = context.Orders.Include(p => p.OrderItems).ToList();
                return orders;
            }
        }

        public List<Product> FillProductList()
        {
            using (var context = new MolotovSportWebContext())
            {
                var product = context.Products.ToList();
                return product;
            }
        }

        public List<User> FillUsersList()
        {
            using (var context = new MolotovSportWebContext())
            {
                var users = context.Users.ToList();
                return users;
            }
        }
    }
}
