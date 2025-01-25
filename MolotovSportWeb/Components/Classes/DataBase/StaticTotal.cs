using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Models;

namespace MolotovSportWeb.Components.Classes.DataBase
{
    static public class StaticTotal
    {
        public static decimal GetTotalPrice(int id)
        {
            using (var context = new MolotovSportWebContext())
            {
                var basket = context.ShopingCarts.Where(p => p.UserId == id).FirstOrDefault();
                int cartId = basket.CartId;
                decimal totalPrice = 0;
                if(basket == null)
                {
                    return 0;
                }
                else
                {
                    List<ShopingItem> ShopingItems = context.ShopingItems.Where(s => s.CartId == cartId).Include(x => x.Product).Include(x => x.Size).ToList();

                    foreach (var item in ShopingItems)
                    {
                        var count = context.ProductSizes.Where(x => x.SizeId == item.SizeId).Select(x => x.Count).First();

                        if (count >= item.Count)
                        {
                            totalPrice += item.Price;
                        }
                        
                    }
                }


                return totalPrice;
            }
        }
    }
}
