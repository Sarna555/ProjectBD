using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Database;
using System.Security.Principal;
using System.Security.Permissions;


namespace Warehouse.Logic
{
    public class Warehouse
    {

        /// <summary>
        /// Dodaje zamówienie bez uwzględnienia pola stanu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reciever"></param>
        /// <param name="sent"></param>
        /// <param name="recieved"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddOrder)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void AddOrder(string sender, string reciever, DateTime sent, DateTime recieved)
        {
            var db = new SQLtoLinqDataContext();
            var order = new Zamowienie();

            order.nadawca = sender;
            order.odbiorca = reciever;
            order.data_nadania = sent;
            order.data_odbioru = recieved;

            db.Zamowienies.InsertOnSubmit(order);
            db.SubmitChanges();
        }


        /// <summary>
        /// Dodaje zamówienie z uwzględnieniem pola stanu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reciever"></param>
        /// <param name="sent"></param>
        /// <param name="recieved"></param>
        /// <param name="state"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddOrder)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void AddOrder(string sender, string reciever, DateTime sent, DateTime recieved, string state)
        {
            var db = new SQLtoLinqDataContext();
            var order = new Zamowienie();

            var stan = (from s in db.Stans
                        where s.nazwa_stanu == state
                        select s).Single();

            order.nadawca = sender;
            order.odbiorca = reciever;
            order.data_nadania = sent;
            order.data_odbioru = recieved;
            order.id_stanu = stan.Id;
            db.Zamowienies.InsertOnSubmit(order);
            db.SubmitChanges();
        }


        /// <summary>
        /// Dodaje paletę do zamówienia bez uwzględnienia jej miejsca w magazynie
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="code"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddPallet)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void AddPallet(string idOrder, string code)
        {
            var db = new SQLtoLinqDataContext();
            var paleta = new Paleta();

            paleta.id_zamowienia = Convert.ToInt32(idOrder);
            paleta.kod = code;

            db.Paletas.InsertOnSubmit(paleta);
            db.SubmitChanges();
        }

        /// <summary>
        /// Dodaje paletę do zamówienia z uwzględnieniem jej miejsca w magazynie
        /// Tabela Miejsce_w_mag ma id(int) i kod(string). Metoda szuka po kodzie, którego id przypisywane
        /// jest do pola id_miejsca_w_mag w obiekcie palety.
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="code"></param>
        /// <param name="spot_code"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddPallet)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void AddPallet(string idOrder, string code, string spot_code)
        {
            var db = new SQLtoLinqDataContext();
            var paleta = new Paleta();

            var spot = (from m in db.Miejsce_w_mags
                        where m.kod == spot_code
                        select m).Single();


            paleta.id_zamowienia = Convert.ToInt32(idOrder);
            paleta.kod = code;
            paleta.id_miejsca_w_mag = spot.Id;

            db.Paletas.InsertOnSubmit(paleta);
            db.SubmitChanges();
        }


        /// <summary>
        /// Dodaje produkt do palety o podanym kodzie 
        /// </summary>
        /// <param name="idPallet"></param>
        /// <param name="name"></param>
        /// <param name="bestBefore"></param>
        /// <param name="category"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddProduct)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void AddProduct(string PalletCode, string name, DateTime bestBefore, string category)
        {
            var db = new SQLtoLinqDataContext();
            var product = new Produkt();

            var cat = (from k in db.Kategoria_produktus
                       where k.nazwa == category
                       select k).Single();

            var pal = (from p in db.Paletas
                       where p.kod == PalletCode
                       select p).Single();

            product.nazwa = name;
            product.id_kategorii = cat.Id;
            product.data_przydatnosci = bestBefore;
            product.id_palety = Convert.ToInt32(pal.Id);

            db.Produkts.InsertOnSubmit(product);
            db.SubmitChanges();
        }

        /// <summary>
        /// Zwraca wszystkie zamówienia
        /// </summary>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllOrders)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static List<OrderResult> GetAllOrders()
        {

            var db = new SQLtoLinqDataContext();
            var OrderResult = (from z in db.Zamowienies
                               from s in db.Stans
                               where z.id_stanu == s.Id
                               select new OrderResult
                               {
                                   Id = z.Id,
                                   nazwa_stanu = s.nazwa_stanu,
                                   nadawca = z.nadawca,
                                   odbiorca = z.odbiorca,
                                   data_nadania = (DateTime)z.data_nadania /*?? DateTime.MinValue*/,
                                   data_odbioru = (DateTime)z.data_odbioru /*?? DateTime.MinValue*/
                               }
                               ).ToList<OrderResult>();
            return OrderResult;
        }

        /// <summary>
        /// Zwraca wszystkie palety w zamówieniu o podanym id.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllPallets)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static List<PalletResult> GetAllPallets(string orderID)
        {

            var db = new SQLtoLinqDataContext();
            var PalletResult = (from p in db.Paletas
                                join m in db.Miejsce_w_mags
                                on p.id_miejsca_w_mag equals m.Id into pallet
                                from m in pallet.DefaultIfEmpty()
                                where p.id_zamowienia == Convert.ToInt32(orderID)
                                select new PalletResult
                                {
                                    Id = p.Id,
                                    kod_miejsca_w_mag = m.kod,
                                    kod_palety = p.kod,
                                    id_zamowienia = Convert.ToInt32(p.id_zamowienia)
                                }).ToList<PalletResult>();
            return PalletResult;
        }

        /// <summary>
        /// Zwraca wszystkie produkty na palecie o podanym kodzie palety.
        /// </summary>
        /// <param name="PalletID"></param>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllProducts)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static List<ProductResult> GetAllProducts(string PalletCode)
        {

            var db = new SQLtoLinqDataContext();

            var pal = (from p in db.Paletas
                       where p.kod == PalletCode
                       select p).Single();

            var ProductResult = (from p in db.Produkts
                                 from k in db.Kategoria_produktus
                                 where p.id_kategorii == k.Id && p.id_palety == pal.Id
                                 select new ProductResult
                                 {
                                     Id = p.Id,
                                     nazwa = p.nazwa,
                                     nazwa_kategorii = k.nazwa,
                                     data_przydatnosci = (DateTime)p.data_przydatnosci
                                 }).ToList<ProductResult>();
            return ProductResult;
        }


        /// <summary>
        /// Aktualizuje dane zamówienia, nie uwzględnia pola id_stanu zamówienia
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="sender"></param>
        /// <param name="reciever"></param>
        /// <param name="sent"></param>
        /// <param name="recieved"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdateOrder)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void UpdateOrder(string orderID, string sender, string reciever, DateTime sent, DateTime recieved)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from z in db.Zamowienies
                          where z.Id == Convert.ToInt32(orderID)
                          select z).Single();
            if (sender != null)
                result.nadawca = sender;
            if (reciever != null)
                result.odbiorca = reciever;
            if (sent != null)
                result.data_nadania = sent;
            if (recieved != null)
                result.data_odbioru = recieved;

            db.SubmitChanges();
        }

        /// <summary>
        /// Aktualizuje dane zamówienia z uwzględnieniem pola id_stanu zamówienia
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="sender"></param>
        /// <param name="reciever"></param>
        /// <param name="sent"></param>
        /// <param name="recieved"></param>
        /// <param name="state"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdateOrder)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void UpdateOrder(string orderID, string sender, string reciever, DateTime sent, DateTime recieved, string state)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from z in db.Zamowienies
                          where z.Id == Convert.ToInt32(orderID)
                          select z).Single();

            if (sender != null)
                result.nadawca = sender;
            if (reciever != null)
                result.odbiorca = reciever;
            if (sent != null)
                result.data_nadania = sent;
            if (recieved != null)
                result.data_odbioru = recieved;
            if (state != null)
            {
                var stan = (from s in db.Stans
                            where s.nazwa_stanu == state
                            select s).Single();
                result.id_stanu = stan.Id;
            }

            db.SubmitChanges();
        }

        /// <summary>
        /// Aktualizuje dane palety bez uwzględnienia id zamówienia oraz id miejsca w magazynie
        /// </summary>
        /// <param name="palletID"></param>
        /// <param name="code"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdatePallet)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void UpdatePallet(string palletID, string code)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from p in db.Paletas
                          where p.Id == Convert.ToInt32(palletID)
                          select p).Single();
            if (code != null)
                result.kod = code;


            db.SubmitChanges();
        }


        /// <summary>
        /// Aktualizuje dane palety z uwzględnieniem id zamówienia oraz id miejsca w magazynie. Te ostatnie jest 
        /// wyciągane w metodzie - jej argument przyjmuje kod miejsca.
        /// </summary>
        /// <param name="palletID"></param>
        /// <param name="code"></param>
        /// <param name="orderID"></param>
        /// <param name="spot_code"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdatePallet)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void UpdatePallet(string palletID, string code, string orderID, string spot_code)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from p in db.Paletas
                          where p.Id == Convert.ToInt32(palletID)
                          select p).Single();



            if (code != null)
                result.kod = code;
            if (orderID != null)
                result.id_zamowienia = Convert.ToInt32(orderID);
            if (spot_code != null)
            {
                var spot = (from m in db.Miejsce_w_mags
                            where m.kod == spot_code
                            select m).Single();
                result.id_miejsca_w_mag = spot.Id;
            }



            db.SubmitChanges();
        }

        /// <summary>
        /// Aktualizuje dane o produkcie
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="name"></param>
        /// <param name="bestBefore"></param>
        /// <param name="category"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.UpdateProduct)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void UpdateProduct(string productID, string name, DateTime bestBefore, string category)
        {
            var db = new SQLtoLinqDataContext();

            var cat = (from k in db.Kategoria_produktus
                       where k.nazwa == category
                       select k).Single();

            var result = (from p in db.Produkts
                          where p.Id == Convert.ToInt32(productID)
                          select p).Single();
            if (name != null)
                result.nazwa = name;
            if (bestBefore != null)
                result.data_przydatnosci = bestBefore;
            if (category != null)
                result.id_kategorii = cat.Id;


            db.SubmitChanges();
        }


        /// <summary>
        /// Zwraca zamówienie o podanym ID
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetOrder)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static OrderResult GetOrder(string orderID)
        {
            var db = new SQLtoLinqDataContext();
            var order = (from z in db.Zamowienies
                         from s in db.Stans
                         where z.id_stanu == s.Id && z.Id == Convert.ToInt32(orderID)
                         select new OrderResult
                         {
                             Id = z.Id,
                             nazwa_stanu = s.nazwa_stanu,
                             nadawca = z.nadawca,
                             odbiorca = z.odbiorca,
                             data_nadania = (DateTime)z.data_nadania,
                             data_odbioru = (DateTime)z.data_odbioru
                         }).Single();
            return order;
        }


        /// <summary>
        /// Zwraca paletę o podanym kodzie
        /// </summary>
        /// <param name="palletCode"></param>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetPallet)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static PalletResult GetPallet(string palletCode)
        {
            var db = new SQLtoLinqDataContext();
            var pall = (from p in db.Paletas
                        join m in db.Miejsce_w_mags
                        on p.id_miejsca_w_mag equals m.Id into pallet
                        from m in pallet.DefaultIfEmpty()
                        where p.kod == palletCode
                        select new PalletResult
                        {
                            Id = p.Id,
                            kod_miejsca_w_mag = m.kod,
                            kod_palety = p.kod,
                            id_zamowienia = Convert.ToInt32(p.id_zamowienia)
                        }).Single();
            return pall;
        }



        /// <summary>
        /// Zwraca produkt o podanym ID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetProduct)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static ProductResult GetProduct(string productID)
        {
            var db = new SQLtoLinqDataContext();
            var product = (from p in db.Produkts
                           from k in db.Kategoria_produktus
                           where p.id_kategorii == k.Id && p.Id == Convert.ToInt32(productID)
                           select new ProductResult
                           {
                               Id = p.Id,
                               nazwa = p.nazwa,
                               nazwa_kategorii = k.nazwa,
                               data_przydatnosci = p.data_przydatnosci ?? DateTime.MinValue
                           }).Single();
            return product;
        }

        /// <summary>
        /// Usuwa zamówienie o podanym ID
        /// </summary>
        /// <param name="orderID"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.DeleteOrder)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void DeleteOrder(string orderID)
        {
            var db = new SQLtoLinqDataContext();
            var order = (from z in db.Zamowienies
                         where z.Id == Convert.ToInt32(orderID)
                         select z).SingleOrDefault();

            List<PalletResult> pall = GetAllPallets(order.Id.ToString());
            foreach (PalletResult element in pall)
            {
                DeletePallet(element.kod_palety);
            }

            db.Zamowienies.DeleteOnSubmit(order);
            db.SubmitChanges();
        }

        /// <summary>
        /// Usuwa paletę o podanym kodzie wraz z wszystkimi produktami na niej
        /// </summary>
        /// <param name="palletCode"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.DeletePallet)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void DeletePallet(string palletCode)
        {
            var db = new SQLtoLinqDataContext();
            var pall = (from p in db.Paletas
                        join m in db.Miejsce_w_mags
                        on p.id_miejsca_w_mag equals m.Id into pallet
                        from m in pallet.DefaultIfEmpty()
                        where p.kod == palletCode
                        select p).SingleOrDefault();

            var products = (from p in db.Produkts
                            where p.id_palety == pall.Id
                            select p);
            db.Produkts.DeleteAllOnSubmit(products);
            db.SubmitChanges();


            db.Paletas.DeleteOnSubmit(pall);
            db.SubmitChanges();
        }


        /// <summary>
        /// Usuwa produkt o podanym ID
        /// </summary>
        /// <param name="productID"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.DeleteProduct)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void DeleteProduct(string productID)
        {
            var db = new SQLtoLinqDataContext();
            var product = (from p in db.Produkts
                           where p.Id == Convert.ToInt32(productID)
                           select p).SingleOrDefault();

            db.Produkts.DeleteOnSubmit(product);
            db.SubmitChanges();
        }


        /// <summary>
        /// zwraca wszystkie występujące kategorie produktu
        /// </summary>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.GetAllCategories)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static List<string> GetAllCategories()
        {
            var db = new SQLtoLinqDataContext();
            var result = (from k in db.Kategoria_produktus
                          select k.nazwa
                               ).ToList<string>();
            return result;

        }

        /// <summary>
        /// Dodaje nową kategorię produktów
        /// </summary>
        /// <param name="name"></param>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.AddCategory)]
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Operation.Admin)]
        public static void AddCategory(string name)
        {
            var db = new SQLtoLinqDataContext();
            var cat = new Kategoria_produktu();

            cat.nazwa = name;
            db.Kategoria_produktus.InsertOnSubmit(cat);
            db.SubmitChanges();
        }


    }
}
