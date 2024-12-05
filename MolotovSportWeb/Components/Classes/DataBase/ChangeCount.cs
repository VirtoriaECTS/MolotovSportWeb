using MolotovSportWeb.Models;

namespace MolotovSportWeb.Components.Classes.DataBase
{
    public class ChangeCount
    {

        public void changeCountAdmin(int sizeId, int count)
        {
            using (var context = new MolotovSportWebContext())
            {
                var sizeChange = context.ProductSizes.Where(p => p.SizeId == sizeId).First();

                sizeChange.Count = count;
                context.SaveChanges();
            }
        }
    }
}
