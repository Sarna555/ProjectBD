using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using Security;


/*
Nadprogramowe funkcje:

    Uwzględniony stan zamówienia (tabela Stan):
        public static void AddOrder2(string sender, string reciever, date sent, date recieved, string state)
        public static void UpdateOrder2(string orderID, string sender, string reciever, date sent, date recieved, string state)

    Uwzględnione miejsce palety w magazynie (tabela Miejsce_w_mag):
        public static void AddPallet2(string idOrder, string code, string spot_code)

    Uwzględnione ww. miejsce oraz id zamówienia
        public static void UpdatePallet2(string palletID, string code, string orderID, string spot_code)

*/


namespace Warehouse.Logic
{
    class Warehouse
    {
        public static void AddOrder(string sender, string reciever, DateTime sent, DateTime recieved)
        {
	        var db = new SQLtoLinqDataContext();
	        var order = new Zamowienie();

	        order.nadawca = sender;
	        order.odbiorca = reciever;
	        order.data_nadania = sent;
	        order.data_odbioru = recieved;

	        db.Zamowienie.InsertOnSubmit(order);
	        db.SubmitChanges();
        }


        //jw, ale z dodaniem stanu zamowienia z tabeli Stan
        public static void AddOrder2(string sender, string reciever, DateTime sent, DateTime recieved, string state)
        {
	        var db = new SQLtoLinqDataContext();
	        var order = new Zamowienie();

	        var stan = (from s in db.Stan
				          where s.nazwa_stanu == state
				          select s).Single();
	
	        order.nadawca = sender;
	        order.odbiorca = reciever;
	        order.data_nadania = sent;
	        order.data_odbioru = recieved;
	        order.id_stanu = stan.id;
	        db.Zamowienie.InsertOnSubmit(order);
	        db.SubmitChanges();
        }

        public static void AddPallet(string idOrder, string code)
        {
	        var db = new SQLtoLinqDataContext();
	        var paleta = new Paleta();

	        paleta.id_zamowienia = idOrder;
	        paleta.kod = code;

	        db.Paleta.InsertOnSubmit(paleta);
	        db.SubmitChanges();
        }

        //jw, ale z dodaniem id miejsca w magazynie
        public static void AddPallet2(string idOrder, string code, string spot_code)
        {
	        var db = new SQLtoLinqDataContext();
	        var paleta = new Paleta();

	        var spot = (from m in db.Miejsce_w_mag
				          where m.kod == spot_code
				          select m).Single();
	
	
	        paleta.id_zamowienia = idOrder;
	        paleta.kod = code;
	        paleta.id_miejsca_w_mag = spot.id;
	
	        db.Paleta.InsertOnSubmit(paleta);
	        db.SubmitChanges();
        }

        public static void AddProduct(string idPallet, string name, DateTime bestBefore, string category)
        {
	        var db = new SQLtoLinqDataContext();
	        var product = new Produkt();

	        var cat = (from k in db.Kategoria_produktu
				          where k.nazwa == category
				          select k).Single();
	
	        product.nazwa = name;
	        product.id_kategorii = cat.id;
	        product.data_przydatnosci = bestBefore;
	        product.id_palety = idPallet;

	        db.Produkt.InsertOnSubmit(product);
	        db.SubmitChanges();
        }

        public static List<OrderResult> GetAllOrders()
        {

	        var db = new SQLtoLinqDataContext();
	        var OrderResult = (from z in db.Zamowienie
						        from s in db.Stan
						        where z.id_stanu == s.Id 
						
					   
					           select new OrderResult
					           {
						           zamowienie_ID = z.Id,
						           stan = s.nazwa_stanu,
						           nadawca = z.nadawca,
						           odbiorca = z.odbiorca
						           data_nadania = z.data_nadania
						           data_odbioru = z.data_odbioru
					           }).ToList<OrderResult>();
	        return OrderResult;
        }


        public static List<PalletResult> GetAllPallets(string orderID)
        {

	        var db = new SQLtoLinqDataContext();
	        var PalletResult = (from p in db.Paleta
						        join m in db.Miejsce_w_magazynie
						        on p.id_miejsca_w_mag equals m.id into pallet
						        from m in pallet.DefaultIfEmpty()
						        where p.id_zamowienia == orderID
					           select new PalletResult
					           {
						           pallet_ID = p.id,
						           kod_miejsca_w_mag = m.kod,
						           kod_palety = p.kod,
						           id_zamowienia = p.id_zamowienia
					           }).ToList<PalletResult>();
	        return PalletResult;
        }

        public static List<ProductResult> GetAllProducts(string PalletID)
        {

	        var db = new SQLtoLinqDataContext();
	        var ProductResult = (from p in db.Produkt
						        from k in db.Kategoria_produktu
						        where p.Id_kategorii == k.Id && p.Id_palety == PalletID
					           select new ProductResult
					           {
						           product_ID = p.Id,
						           nazwa = p.Nazwa,
						           nazwa_kategorii = k.Nazwa,
						           data_przydatnosci = p.Data_przydatnosci
					           }).ToList<ProductResult>();
	        return ProductResult;
        }

        public static void UpdateOrder(string orderID, string sender, string reciever, DateTime sent, DateTime recieved)
        {
	        var db = new SQLtoLinqDataContext();
	        var result = (from z in db.Zamowienie
				          where z.id == orderID
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

        //jw, ale z uwzglednieniem zmiany stanu zamowienia z tabeli Stan
        public static void UpdateOrder2(string orderID, string sender, string reciever, DateTime sent, DateTime recieved, string state)
        {
	        var db = new SQLtoLinqDataContext();
	        var result = (from z in db.Zamowienie
				          where z.id == orderID
				          select z).Single();
				  
				  
				  
				  
	        if (sender != null)
		        result.nadawca = sender;
	        if (reciever != null)
		        result.odbiorca = reciever;
	        if (sent != null)
		        result.data_nadania = sent;
	        if (recieved != null)
		        result.data_odbioru = recieved;
	        if (state != null){
		        var stan = (from s in db.Stan
				          where s.nazwa_stanu == state
				          select s).Single();
		        result.id_stanu = stan.id;
	        }	
		
	        db.SubmitChanges();
        }

        public static void UpdatePallet(string palletID, string code)
        {
	        var db = new SQLtoLinqDataContext();
	        var result = (from p in db.Paleta
				          where p.id == palletID
				          select p).Single();
	        if (code != null)
		        result.Kod = code;
	

	        db.SubmitChanges();
        }

        //jw, ale z uwzglednieniem zmiany id zamowienia oraz id miejsca w magazynie
        public static void UpdatePallet2(string palletID, string code, string orderID, string spot_code)
        {
	        var db = new SQLtoLinqDataContext();
	        var result = (from p in db.Paleta
				          where p.id == palletID
				          select p).Single();
				  
			  
				  
	        if (code != null)
		        result.Kod = code;
	        if (orderID != null)
		        result.id_zamowienia = orderID;
	        if (spot_code != null){
		        var spot = (from m in db.Miejsce_w_mag
				          where m.kod == spot_code
				          select m).Single();	
		        result.id_miejsca_w_mag = spot.id;
	        }
	
	

	        db.SubmitChanges();
        }


        public static void UpdateProduct(string productID, string name, DateTime bestBefore, string category)
        {
	        var db = new SQLtoLinqDataContext();
	
	        var cat = (from k in db.Kategoria_produktu
				          where k.nazwa == category
				          select k).Single();
	
	        var result = (from p in db.Produkt
				          where p.id == productID
				          select p).Single();
	        if (name != null)
		        result.nazwa = name;
	        if (bestBefore != null)
		        result.data_przydatnosci = bestBefore;
	        if (category != null)
		        result.id_kategorii = cat.id;


	        db.SubmitChanges();
        }

        public static OrderResult GetOrder(string orderID)
        {
	        var db = new SQLtoLinqDataContext();
	        var order = (from z in db.Zamowienie
				        from s in db.Stan
				        where z.id_stanu == s.Id && z.id == orderID
				        select new UserResult
				        {
				           zamowienie_ID = z.Id,
				           stan = s.nazwa_stanu,
				           nadawca = z.nadawca,
				           odbiorca = z.odbiorca
				           data_nadania = z.data_nadania
				           data_odbioru = z.data_odbioru
				           id_palety = z.id_palety
				        }).Single();
	        return order;
        }

        public static PalletResult GetPallet(string palletID)
        {
	        var db = new SQLtoLinqDataContext();
	        var pallet = (from p in db.Paleta
					        join m in db.Miejsce_w_magazynie
					        on p.id_miejsca_w_mag equals m.id into pallet
					        from m in pallet.DefaultIfEmpty()
					        where p.id_zamowienia == orderID
				           select new PalletResult
				           {
					           pallet_ID = p.id,
					           kod_miejsca_w_mag = m.kod,
					           kod_palety = p.kod,
					           id_zamowienia = p.id_zamowienia
				           }).Single();
	        return pallet;
        }	


        public static GetProduct(string productID)
        {
	        var db = new SQLtoLinqDataContext();
	        var product = (from p in db.Produkt
				        from k in db.Kategoria_produktu
				        where p.Id_kategorii == k.Id && p.id == productID
				        select new ProductResult
				        {
				           product_ID = p.Id,
				           nazwa = p.Nazwa,
				           nazwa_kategorii = k.Nazwa,
				           data_przydatnosci = p.Data_przydatnosci
				        }).Single();
	        return product;
        }		
		
		
        public static void DeleteOrder(string orderID)
        {
	        var db = new SQLtoLinqDataContext();
	        var order = (from z in db.Zamowienie
				        where z.id == orderID
				        select z).Single();

	        db.Zamowienie.DeleteOnSubmit(order);
	        db.SubmitChanges();
        }	

        //usuwa paletę wraz z produktami na niej
        public static void DeletePallet(string palletID)
        {
	        var db = new SQLtoLinqDataContext();
	        var pallet = (from p in db.Paleta
				        where p.id == palletID
				        select p).Single();

	        var products = (from p in db.Produkt
				        where p.id_palety == pallet.id
				        select p);
	        db.Produkt.DeleteAllOnSubmit(products);
	        db.SubmitChanges();			
				
				
	        db.Paleta.DeleteOnSubmit(pallet);
	        db.SubmitChanges();
        }	

        public static void DeleteProduct(string productID)
        {
	        var db = new SQLtoLinqDataContext();
	        var product = (from p in db.Produkt
				        where p.id == productID
				        select p).Single();

	        db.Produkt.DeleteOnSubmit(product);
	        db.SubmitChanges();
        }	


    }
}
