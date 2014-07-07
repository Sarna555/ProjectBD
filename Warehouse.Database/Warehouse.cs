using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class Warehouse
    {

        public static void GetAllItems()
        {
            SQLtoLinqDataContext db = new SQLtoLinqDataContext();
            var products = (from p in db.Produkts
                            from p1 in db.Paletas
                            from p2 in db.Kategoria_produktus
                            where p.id_palety == p.prod
                            select new ProductResult
                            {
                                name = p.nazwa,
                                pallet = p1.kod,
                                date = (DateTime)p.data_przydatnosci,

                            }).ToList<ProductResult>();
        }
    }
}
