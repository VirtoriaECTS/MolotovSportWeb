using MolotovSportWeb.Models;

namespace MolotovSportWeb.Components.Classes.Statistic
{
    public class Statistic
    {
        public List<double> priceMonth(DateTime selectMonth)
        {
            List<double> money = new List<double>();
            using (var context = new MolotovSportWebContext())
            {

                var order = context.Orders.Where(x => x.OrderData > selectMonth).GroupBy(x => x.OrderData);


                foreach(var day in order)
                {
                    double sum = day.Sum(x => x.TotalAmout);
                    money.Add(sum);
                }
            }
            return money;
        }
    }
}
