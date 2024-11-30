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




        public void NewProductAddBasket(Product SelectedProduct, int idSize, int priceProduct)
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

                shopingCart.TotalAmout = shopingCart.TotalAmout + priceProduct;
                context.Update(shopingCart);

                context.SaveChanges();

            }
        }


        public void DeleteProduct(int id, int priceProduct)
        {
            using (var context = new MolotovSportWebContext())
            {

                var basket = context.ShopingCarts.Where(u => u.UserId == UserId).First();
                basket.TotalAmout -= priceProduct;
                var item2 = context.ShopingItems.Where(p => p.CardItemId == id).First();
                context.Remove(item2);
                context.Update(basket);
                context.SaveChanges();
            }
        }

        public void ChangeMinusCount(ShopingItem item, int priceProduct)
        {
            using (var context = new MolotovSportWebContext())
            {
                if (item.Count == 1)
                {
                    DeleteProduct(item.CardItemId,priceProduct);

                }
                else
                {
                    item.Count = item.Count - 1;
                    item.Price -= priceProduct;
                    context.Update(item);
                    
                    var basket = context.ShopingCarts.Where(u => u.UserId == UserId).First();
                    basket.TotalAmout -= priceProduct;
                    context.Update(basket);

                    context.SaveChanges();

                }
            }
        }

        public void ChangePlusCount(ShopingItem item, int priceProduct)
        {
            using (var context = new MolotovSportWebContext())
            {
                item.Count = item.Count + 1;
                item.Price += priceProduct;
                context.Update(item);

                var basket = context.ShopingCarts.Where(u => u.UserId == UserId).First();
                basket.TotalAmout += priceProduct;
                context.Update(basket);

                context.SaveChanges();
            }
        }
    }
}
