namespace MolotovSportWeb.Components.Classes.DataBase
{
    using Microsoft.EntityFrameworkCore;
    using MolotovSportWeb.Models;

    public class AddInfo
    {
        public void AddProduct(string name, int IdFirm, int IdCaregory, int IdCaregoryMini, int idGender, string image, int price, List<string> Size)
        {
            using (var context = new MolotovSportWebContext())
            {

                
            }
        }
    }
}
