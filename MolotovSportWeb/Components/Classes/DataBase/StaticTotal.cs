﻿using MolotovSportWeb.Models;

namespace MolotovSportWeb.Components.Classes.DataBase
{
    static public class StaticTotal
    {
        public static decimal GetTotalPrice(int id)
        {
            using (var context = new MolotovSportWebContext())
            {
                var basket = context.ShopingCarts.Where(p => p.UserId == id).FirstOrDefault();
                return basket.TotalAmout;
            }
        }
    }
}
