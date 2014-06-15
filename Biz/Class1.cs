using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Logic
{
    public class Class1
    {
        /// <summary>
        /// Twoja stara lel not really
        /// </summary>
        public Class1()
        {
            
        }

        public static IEnumerable<String> showAll()
        {
            sqltolinqdata db = new DataClasses1DataContext();
            var userResults = from p in db.Users
                              where p.name == "Rafal"
                              select p.surname;
            return userResults;
        }
    }
}
