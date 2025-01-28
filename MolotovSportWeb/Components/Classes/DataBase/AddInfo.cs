namespace MolotovSportWeb.Components.Classes.DataBase
{
    using Microsoft.EntityFrameworkCore;
    using MolotovSportWeb.Models;
    using System.Collections.Generic;

    public class AddInfo
    {
        public void AddProduct(string name, int IdFirm, int IdCaregory, int IdCaregoryMini, int idGender, string image, int price, Dictionary<string, int> Size)
        {
            using (var context = new MolotovSportWebContext())
            {
                var product = new Product
                {
                    ProductName = name,
                    FirmId = IdFirm,
                    CategoryId = IdCaregory,
                    CategoryIdMini = IdCaregoryMini,
                    GenderId = idGender,
                    ImageUrl = image,
                    Price = price,
                };

                context.Products.Add(product);
                context.SaveChanges();

                var productNew = context.Products.Where(p => p.ProductName == name).Where(p => p.FirmId == IdFirm).Where(p => p.CategoryId == IdCaregory).Where(p => p.GenderId == idGender)
                    .Where(p => p.ImageUrl == image).Where(p => p.Price == price).FirstOrDefault();


                foreach (var item in Size)
                {
                    var sizeNew = new ProductSize
                    {
                        ProductId = productNew.ProductId,
                        Size = item.Key,
                        //Count = item.Value
                        Count = 50
                    };
                    context.Add(sizeNew);
                }

                context.SaveChanges();
            }
        }

        public void DeleteFirm(int idFirm)
        {
            using (var context = new MolotovSportWebContext())
            {
                var listProduct = context.Products.Where(p => p.FirmId == idFirm).Include(x => x.ProductSizes).ToList();
                var allSize = context.ProductSizes;

                foreach (var item in listProduct)
                {
                    var sizedelete = allSize.Where(x => x.ProductId == item.ProductId).ForEachAsync(x => x.Count = -1);
                    
                }

                
                context.SaveChanges();
            }

        }

        public void AddFirm(string nameFirm)
        {
            using (var context = new MolotovSportWebContext())
            {
                var firm = new Firm
                {
                    FirmName = nameFirm,
                };
                context.Add(firm);
                context.SaveChanges();
            }
        }

        public void AddCategoryMini(int idCategory, string categoryMiniNameText)
        {
            using (var context = new MolotovSportWebContext())
            {
                var categoryMini = new CategoriesMini
                {
                    CategoryId = idCategory,
                    CategoryMiniName = categoryMiniNameText,
                };
                context.Add(categoryMini);
                context.SaveChanges();
            }
        }

       

        public void DeleteCategoryMin(int idCategoryMini)
        {
            using (var context = new MolotovSportWebContext())
            {
                var listProduct = context.Products.Where(p => p.CategoryIdMini == idCategoryMini).Include(x => x.ProductSizes).ToList();
                var allSize = context.ProductSizes;

                foreach (var item in listProduct)
                {
                    var sizedelete = allSize.Where(x => x.ProductId == item.ProductId).ForEachAsync(x => x.Count = -1);

                }


                context.SaveChanges();
            }

        }
    }
}
