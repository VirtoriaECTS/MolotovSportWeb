namespace MolotovSportWeb.Components.Classes.CheakData
{
    public class CheakNewProduct
    {
        public bool cheakPrice(string price)
        {
            try
            {
                int number = Convert.ToInt32(price);
                if(number <= 0) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool cheakName(string name)
        {
            if(name.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
