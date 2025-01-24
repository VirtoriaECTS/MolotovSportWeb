using Microsoft.EntityFrameworkCore;
using MolotovSportWeb.Models;
using System.Linq;

namespace MolotovSportWeb.Components.Classes.Statistic
{
    public class Statistic
    {
        public List<double> priceMonth(DateTime selectMonth, DateTime selectMonth2)
        {
            List<double> money = new List<double>();
            using (var context = new MolotovSportWebContext())
            {

                var order = context.Orders.Where(x => x.OrderData > selectMonth  && x.OrderData < selectMonth2).GroupBy(x => x.OrderData.Date);


                foreach(var day in order)
                {
                    double sum = day.Sum(x => x.TotalAmout);
                    money.Add(sum);
                }
            }
            return money;
        }

        public List<double> priceYear()
        {
            List<double> money = new List<double>();
            using (var context = new MolotovSportWebContext())
            {

                var order = context.Orders.GroupBy(x => x.OrderData.Date);


                foreach (var day in order)
                {
                    double sum = day.Sum(x => x.TotalAmout);
                    money.Add(sum);
                }
            }
            return money;
        }
        public List<string> legenMoneyAllTime()
        {
            List<string> dateString = new List<string>();
            using (var context = new MolotovSportWebContext())
            {



                var order = context.Orders.GroupBy(x => x.OrderData.Date);


                foreach (var day in order)
                {
                    DateTime date = day.Select(x => x.OrderData).FirstOrDefault();
                    dateString.Add(date.ToString("dd/M/yy"));
                }


            }

            return dateString;
        }

        public List<string> legenMoneyDate(DateTime selectMonth, DateTime selectMonth2)
        {
            List<string> dateString = new List<string>();
            using (var context = new MolotovSportWebContext())
            {



                var order = context.Orders.Where(x => x.OrderData > selectMonth && x.OrderData < selectMonth2).GroupBy(x => x.OrderData.Date);


                foreach (var day in order)
                {
                    DateTime date = day.Select(x => x.OrderData).FirstOrDefault();
                    dateString.Add(date.ToString("dd/M/yy"));
                }
            }

            return dateString;
        }


        public List<double> countYear()
        {
            List<double> countSell = new List<double>();


            using (var context = new MolotovSportWebContext())
            {

                var order = context.Orders.GroupBy(x => x.OrderData.Date);
                var allOrder = context.Orders;
                var orderItems = context.OrderItems;

                List<DateTime> allDateTime = new List<DateTime>();
                foreach (var day in order)
                {
                    allDateTime.Add(day.Select(x => x.OrderData.Date).First());


                }

                foreach(var oneDate in allDateTime)
                {
                    List<int> DayOrderdId = allOrder.Where(x => x.OrderData.Date == oneDate.Date).Select(x => x.OrderId).ToList();

                    int count = 0;
                    foreach (var id in DayOrderdId)
                    {
                        count += orderItems.Where(x => x.OrderId == id).Sum(x => x.Count);
                    }
                    countSell.Add(count);

                }

            }


            return countSell;
        }


        public List<double> countMonth(DateTime selectMonth, DateTime selectMonth2)
        {
            List<double> countSell = new List<double>();


            using (var context = new MolotovSportWebContext())
            {

                var order = context.Orders.Where(x => x.OrderData > selectMonth && x.OrderData < selectMonth2).GroupBy(x => x.OrderData.Date);
                var allOrder = context.Orders;
                var orderItems = context.OrderItems;

                List<DateTime> allDateTime = new List<DateTime>();
                foreach (var day in order)
                {
                    allDateTime.Add(day.Select(x => x.OrderData.Date).First());


                }

                foreach (var oneDate in allDateTime)
                {
                    List<int> DayOrderdId = allOrder.Where(x => x.OrderData.Date == oneDate.Date).Select(x => x.OrderId).ToList();

                    int count = 0;
                    foreach (var id in DayOrderdId)
                    {
                        count += orderItems.Where(x => x.OrderId == id).Sum(x => x.Count);
                    }
                    countSell.Add(count);

                }

            }


            return countSell;
        }


        public Dictionary<string, int> popularProduct()
        {
            Dictionary<string, int> popularDictonary = new Dictionary<string, int>();

            Dictionary<int, int> popularproduct= new Dictionary<int, int>();

            using (var context = new MolotovSportWebContext())
            {

                var orderItems = context.OrderItems;
 


                foreach (var order in orderItems)
                {
                    if (popularproduct.ContainsKey(Convert.ToInt32(order.ProductId)))
                    {
                        popularproduct[order.ProductId] += order.Count;
                    }
                    else
                    {
                        popularproduct[order.ProductId] = order.Count;
                    }
                }


                var productsFirm = context.Products;

                foreach (var pair in popularproduct.OrderByDescending(pair => pair.Value).Take(10))
                {

                    int productNameId = pair.Key;
                    string productName = productsFirm.Where(x => x.ProductId == productNameId).Select(x => x.ProductName).First();
                    int count = pair.Value;

                    popularDictonary[productName] = count;

                    
                }

            }


            return popularDictonary;
        }

        public Dictionary<string, int> popularBrend()
        {
            Dictionary<string, int> sortBrends = new Dictionary<string, int>();
            Dictionary<string, int> popularDictonary = new Dictionary<string, int>();

            Dictionary<int, int> popularproduct = new Dictionary<int, int>();

            using (var context = new MolotovSportWebContext())
            {

                var orderItems = context.OrderItems;
                var product = context.Products;
                var firm = context.Firms;



                foreach (var order in orderItems)
                {
                    if (popularproduct.ContainsKey(Convert.ToInt32(order.ProductId)))
                    {
                        popularproduct[order.ProductId] += order.Count;
                    }
                    else
                    {
                        popularproduct[order.ProductId] = order.Count;
                    }
                }


                var productsFirm = context.Products;

                foreach (var pair in popularproduct.OrderByDescending(pair => pair.Value))
                {

                    int productNameId = pair.Key;
                    int count = pair.Value;

                    int firmId = product.Where(x => x.ProductId == productNameId).Select(x => x.FirmId).First();

                    string nameFirm = firm.Where(x => x.FirmId == firmId).Select(x => x.FirmName).First();

                    if (popularDictonary.ContainsKey(nameFirm))
                    {
                        popularDictonary[nameFirm] += count;
                    }
                    else
                    {
                        popularDictonary[nameFirm] = count;
                    }

                }

                foreach (var pair in popularDictonary.OrderByDescending(pair => pair.Value))
                {

                    sortBrends[pair.Key] = pair.Value;

                }



            }

            return sortBrends ;
        }





    }
}
