using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Models;


namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class FilrtAndSorted
    {
        private int IdCategoryScreen;
        MolotovSportWebContext context;

        private List<int> CheackListGender = new List<int>();
        private List<int> CheakListCategoryMini = new List<int>();
        private List<int> CheackListFirms = new List<int>();


        public List<MolotovSportWeb.Models.Product> Products = new();

        public List<MolotovSportWeb.Models.Gender> GenderList = new();

        public List<MolotovSportWeb.Models.CategoriesMini> CategoryiesMini = new();

        public List<MolotovSportWeb.Models.Firm> FirmList = new();

        public  FilrtAndSorted(int IdCategoryScreen)
        {
            context = new MolotovSportWebContext();
            this.IdCategoryScreen = IdCategoryScreen;
            LoadingDataBaseAsync();

        }

        public  void LoadingDataBaseAsync()
        {
            Products =  context.Products.Where(p => p.CategoryId == IdCategoryScreen).ToList();
            CategoryiesMini =  context.CategoriesMinis.Where(p => p.CategoryId == IdCategoryScreen).ToList();
            GenderList =  context.Genders.ToList();
            FirmList =  context.Firms.ToList();
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
    }
}
