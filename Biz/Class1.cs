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

        public static List<UserResult> showAll()
        {
            
            SQLtoLinqDataContext db = new SQLtoLinqDataContext();
            var userResults = (from p in db.Users
                              select new UserResult
                              {
                                  user_ID = p.user_ID,
                                  name = p.name,
                                  surname = p.surname,
                                  email = p.email
                              }).ToList<UserResult>();
            return userResults;
        }

        public static List<UserResult> FindUser(String email, String password)
        {
            SQLtoLinqDataContext db = new SQLtoLinqDataContext();
            var userResult = (from p in db.Users
                             where p.email == email && p.password == password
                             select new UserResult
                             {
                                 user_ID = p.user_ID,
                                 name = p.name,
                                 surname = p.surname,
                                 email = p.email
                             }).ToList<UserResult>();
            return userResult;
        }
    }
}
