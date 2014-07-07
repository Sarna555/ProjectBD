using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Security.Principal;
using System.Security.Permissions;
using Security;

namespace Admin.Logic
{
    public class Administration
    {
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
        public static List<UserResult> GetAll()
        {

            SQLtoLinqDataContext db = new SQLtoLinqDataContext();
            var userResults = (from p in db.Users
                               select new UserResult
                               {
                                   user_ID = p.user_ID,
                                   name = p.name,
                                   surname = p.surname,
                                   login = p.login
                               }).ToList<UserResult>();
            return userResults;
        }
    }
}
