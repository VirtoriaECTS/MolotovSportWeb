using MolotovSportWeb.Models;
using static MudBlazor.CategoryTypes;

namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class CreateOrder

    {

        int UserId;

        public CreateOrder(int UserId)
        {
            this.UserId = UserId;
        }

        public async void Create(string text)
        {
            string[] array = text.Split('|');

            Classes.Servers.YandexGeocoderService yandexGeocoderService;




            using (var context = new MolotovSportWebContext())
            {
                DateTime curDate = DateTime.Now; 
                var shoppingCart = context.ShopingCarts.Where(p => p.UserId == UserId).First();

                var shopingItemsAll = context.ShopingItems.Where(p => p.CartId == shoppingCart.CartId).ToList();
                var allSize = context.ProductSizes.ToList();
                List<ShopingItem> shopingItems = new List<ShopingItem>();

                foreach (var item in context.ShopingItems.Where(p => p.CartId == shoppingCart.CartId))
                {
                    int sizeId = item.SizeId;
                    var count = allSize.Where(x => x.SizeId == sizeId).Select(x => x.Count).FirstOrDefault();
                    if(count >= item.Count)
                    {
                        shopingItems.Add(item);
                    }
                }



                var sizeAll = context.ProductSizes;

                var order = new Order

                {    
                    UserId = UserId,
                    OrderData = curDate,

                    TotalAmout = Convert.ToInt32(Classes.DataBase.StaticTotal.GetTotalPrice(UserId)) + 500,
                    StatusOrder = false,
                    Adress = array[0],
                    PriceDeliviry = Convert.ToInt32(array[1])

                };
                context.Orders.Add(order);
                context.SaveChanges();
                

                var newOrder = context.Orders.Where(p => p.UserId == order.UserId).Where(p => p.OrderData == order.OrderData)
                //.Where(p => p.TotalAmout == order.TotalAmout).Where(p => p.StatusOrder == order.StatusOrder).Where(p => p.Adress == order.Adress).Where(p => p.PriceDeliviry == order.PriceDeliviry)
                    .First();
                foreach (var item in shopingItems)
                {
                    
                    var sizeCount = sizeAll.Where(x => x.SizeId == item.SizeId).First();
                    sizeCount.Count -= item.Count;
                    context.SaveChanges();
                }



                foreach (var item in shopingItems)

                {

                    context.SaveChanges();

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



                context.SaveChanges();

                foreach (var item in shopingItems)

                {
                    context.ShopingItems.Remove(item);
                }
                context.SaveChanges();


                
                shoppingCart.TotalAmout = 0;
                context.SaveChanges();

            }
        }
    }
}
