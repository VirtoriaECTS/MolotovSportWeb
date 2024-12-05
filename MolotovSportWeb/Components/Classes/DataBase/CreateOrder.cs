using MolotovSportWeb.Models;

namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class CreateOrder

    {

        int UserId;

        public CreateOrder(int UserId)
        {
            this.UserId = UserId;
        }

        public void Create(string adress)
        {
            using (var context = new MolotovSportWebContext())
            {
                DateTime curDate = DateTime.Now;
                var shoppingCart = context.ShopingCarts.Where(p => p.UserId == UserId).First();
                var shopingItems = context.ShopingItems.Where(p => p.CartId == shoppingCart.CartId);

                var order = new Order

                {    
                    UserId = UserId,
                    OrderData = curDate,

                    TotalAmout = Convert.ToInt32(shoppingCart.TotalAmout) + 500,
                    StatusOrder = false,
                    Adress = adress,
                    PriceDeliviry = 500

                };
                context.Orders.Add(order);
                context.SaveChanges();

                var newOrder = context.Orders.Where(p => p.UserId == order.UserId).Where(p => p.OrderData == order.OrderData)
                    //.Where(p => p.TotalAmout == order.TotalAmout).Where(p => p.StatusOrder == order.StatusOrder).Where(p => p.Adress == order.Adress).Where(p => p.PriceDeliviry == order.PriceDeliviry)
                    .First();

                foreach (var item in shopingItems)

                {

                    var newItemOrder = new OrderItem
                    {
                        OrderId = newOrder.OrderId,
                        ProductId = item.ProductId,
                        SizeId = item.SizeId,
                        Count = item.Count,
                        Price = item.Price
                    };
                    context.OrderItems.Add(newItemOrder);


                }
                context.SaveChanges();

                foreach (var item in shopingItems)

                {
                    context.ShopingItems.Remove(item);
                }


                shoppingCart.TotalAmout = 0;
                context.SaveChanges();

            }
        }
    }
}
