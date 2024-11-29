using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Components.Classes.Servers;
using MolotovSportWeb.Models;


namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class ChangeProductUser
    {

        int UserId;

        public ChangeProductUser(int UserId)
        {
            this.UserId = UserId;
        }

        public int GetPriceProduct(int idProduct)
        {
            using (var context = new MolotovSportWebContext())
            {
                var product = context.Products.Where(p => p.ProductId == idProduct).FirstOrDefault();
                int price = Convert.ToInt32(product.Price);
                return price;
            }
        }


        public void NewProductAddBasket(Product SelectedProduct, int idSize)
        {
            using (var context = new MolotovSportWebContext())
            {
                ShopingCart shopingCart = context.ShopingCarts.Where(u => u.UserId == UserId).First();

                var shopintItems = new ShopingItem
                {

                    CartId = shopingCart.CartId,
                    ProductId = SelectedProduct.ProductId,
                    SizeId = idSize,
                    Count = 1,
                    Price = SelectedProduct.Price
                };

                context.ShopingItems.Add(shopintItems);
                context.SaveChanges();

                shopingCart.TotalAmout = shopingCart.TotalAmout + GetPriceProduct(SelectedProduct.ProductId);
                context.Update(shopingCart);

                context.SaveChanges();

            }
        }


        public void DeleteProduct(int id)
        {
            using (var context = new MolotovSportWebContext())
            {
                int price = GetPriceProduct(id);
                var basket = context.ShopingCarts.Where(u => u.UserId == UserId).First();
                basket.TotalAmout -= price;
                var item2 = context.ShopingItems.Where(p => p.CardItemId == id).First();
                context.Remove(item2);
                context.Update(basket);
                context.SaveChanges();
            }
        }

        public void ChangeMinusCount(ShopingItem item)
        {
            using (var context = new MolotovSportWebContext())
            {
                if (item.Count == 1)
                {
                    DeleteProduct(item.CardItemId);

                }
                else
                {
                    int price = GetPriceProduct(item.ProductId);
                    item.Count = item.Count - 1;
                    item.Price -= price;
                    context.Update(item);
                    
                    var basket = context.ShopingCarts.Where(u => u.UserId == UserId).First();
                    basket.TotalAmout -= price;
                    context.Update(basket);

                    context.SaveChanges();

                }
            }
        }

        public void ChangePlusCount(ShopingItem item)
        {
            using (var context = new MolotovSportWebContext())
            {
                int price = GetPriceProduct(item.ProductId);
                item.Count = item.Count + 1;
                item.Price += price;
                context.Update(item);

                var basket = context.ShopingCarts.Where(u => u.UserId == UserId).First();
                basket.TotalAmout += price;
                context.Update(basket);

                context.SaveChanges();
            }
        }
    }
}
