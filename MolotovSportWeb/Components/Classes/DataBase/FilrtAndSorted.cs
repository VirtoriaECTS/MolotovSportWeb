﻿using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Models;
using System.Collections.Concurrent;


namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class FilrtAndSorted
    {
        private int IdCategoryScreen;
        private string searchText;

        MolotovSportWebContext context;


        private List<int> CheackListGender = new List<int>();
        private List<int> CheakListCategoryMini = new List<int>();
        private List<int> CheackListFirms = new List<int>();
        public int SortedValue = 0;


        public List<MolotovSportWeb.Models.Product> Products = new();

        public List<MolotovSportWeb.Models.Gender> GenderList = new();

        public List<MolotovSportWeb.Models.CategoriesMini> CategoryiesMini = new();

        public List<MolotovSportWeb.Models.Category> Categoryies= new();

        public List<MolotovSportWeb.Models.Firm> FirmList = new();

        public  FilrtAndSorted(int IdCategoryScreen)
        {
            context = new MolotovSportWebContext();
            this.IdCategoryScreen = IdCategoryScreen;
            LoadingDataBaseAsync();

        }

        public FilrtAndSorted(string searchText)
        {
            context = new MolotovSportWebContext();
            this.IdCategoryScreen = 0;
            this.searchText = searchText;
            LoadingDataBaseAsync();

        }



        public  void LoadingDataBaseAsync()
        {
            if(IdCategoryScreen > 0)
            {
                Products = context.Products.Where(p => p.CategoryId == IdCategoryScreen)
                .Include(p => p.ProductSizes).Where(p => p.ProductSizes.Any(p => p.Count > 0))
                .ToList();


                CategoryiesMini = ActualyCategoryMini().Where(p => p.CategoryId == IdCategoryScreen).ToList();
                GenderList = context.Genders.ToList();
                FirmList = ActualyFirm();
            }

            else if(IdCategoryScreen == 0) 
            {
                Products = context.Products.Where(p => EF.Functions.Like(p.ProductName.ToLower(), $"%{searchText.ToLower()}%"))
                .Include(p => p.ProductSizes).Where(p => p.ProductSizes.Any(p => p.Count > 0))
                .ToList();


                CategoryiesMini = ActualyCategoryMini();
                GenderList = context.Genders.ToList();
                FirmList = ActualyFirm(); ;
            }
            else if (IdCategoryScreen == -1)
            {
                Products = context.Products
                .Include(p => p.ProductSizes).Include(p => p.Gender).Include(p => p.Firm)
                .Include(p => p.CategoriesMini).Include(p => p.CategoriesMini.Category)
                .Where(p => p.ProductSizes.Any(p => p.Count > -1))
                .ToList();

                Categoryies = context.Categories.ToList();
                CategoryiesMini = ActualyCategoryMini();
                GenderList = context.Genders.ToList();
                FirmList = ActualyFirm();
            }
        }

        public List<Firm> ActualyFirm()
        {
            List<Firm> actialfirms = new List<Firm>();
            List<Firm> allFirm = context.Firms.ToList();
            List<Product> allProduct = context.Products.Include(x => x.ProductSizes).ToList();


            foreach (Firm firm in allFirm)
            {
                var productFirmSize = allProduct.Where(x => x.FirmId == firm.FirmId).Select(x => x.ProductSizes).ToList();
                if (productFirmSize.Count == 0)
                {                
                    actialfirms.Add(firm);
                    continue;
                }


                foreach (var size in productFirmSize)
                {
                    int count = size.Sum(x => x.Count);
                    if(count >= 0)
                    {
                        actialfirms.Add(firm);
                        break;
                    }
                }
            }
                return actialfirms;
        }

        public List<CategoriesMini> ActualyCategoryMini()
        {
            List<CategoriesMini> actialcategoryMini = new List<CategoriesMini>();
            List<CategoriesMini> allCategoryMini = context.CategoriesMinis.ToList();
            List<Product> allProduct = context.Products.Include(x => x.ProductSizes).ToList();


            foreach (CategoriesMini category in allCategoryMini)
            {
                var productCategorySize = allProduct.Where(x => x.CategoryIdMini == category.CategoryMiniId).Select(x => x.ProductSizes).ToList();
                if (productCategorySize.Count == 0)
                {
                    actialcategoryMini.Add(category);
                    continue;
                }


                foreach (var size in productCategorySize)
                {
                    int count = size.Sum(x => x.Count);
                    if (count >= 0)
                    {
                        actialcategoryMini.Add(category);
                        break;
                    }
                }
            }
            return actialcategoryMini;
        }


        public async void UpdateFiltr()
        {
             LoadingDataBaseAsync();
            foreach (int gender in CheackListGender)
            {
                Products = Products.Where(p => CheackListGender.Contains(p.GenderId)).ToList();

            }
            foreach (int category in CheakListCategoryMini)
            {
                Products = Products.Where(p => CheakListCategoryMini.Contains(p.CategoryIdMini)).ToList();

            }
            foreach (int firm in CheackListFirms)
            {
                Products = Products.Where(p => CheackListFirms.Contains(p.FirmId)).ToList();

            }

            if(SortedValue == 1)
            {
                Products = Products.OrderBy(p => p.Price).ToList();
            }

            if (SortedValue == -1)
            {
                Products = Products.OrderByDescending(p => p.Price).ToList();
            }

        }

        public void SortderGenderCheakBox(int id)
        {
            if (CheackListGender.Contains(id))
            {
                CheackListGender.Remove(id);
            }
            else
            {
                CheackListGender.Add(id);
            }
            UpdateFiltr();

        }

        public void SortderFirmsCheakBox(int id)
        {
            if (CheackListFirms.Contains(id))
            {
                CheackListFirms.Remove(id);
            }
            else
            {
                CheackListFirms.Add(id);
            }
            UpdateFiltr();

        }

        public void SortderCategoryCheakBox(int id)
        {
            if (CheakListCategoryMini.Contains(id))
            {
                CheakListCategoryMini.Remove(id);
            }
            else
            {
                CheakListCategoryMini.Add(id);
            }
            UpdateFiltr();

        }

        public void SortDataBase(int id)
        {
            SortedValue = id;
            UpdateFiltr() ;
        }
    }
}
