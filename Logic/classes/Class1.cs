using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Security.Principal;
using System.Security.Permissions;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
                                  email = p.email,
                                  operations = (from p1 in db.operations
                                                from p2 in db.users2operations
                                                where p1.operation_ID == p2.operation_ID && p.user_ID == p2.user_ID
                                                select p1.name).ToList<String>(),
                                  groups = (from p1 in db.groups
                                            from p2 in db.users2groups
                                            where p1.group_ID == p2.group_ID && p.user_ID == p2.user_ID
                                            select new GroupResult
                                            {   
                                                operations = 
                                                (from p3 in db.operations
                                                from p4 in db.groups2operations
                                                where p3.operation_ID == p4.operation_ID && p1.group_ID == p4.group_ID
                                                select p3.name).ToList<String>()
                                            }).ToList<GroupResult>()
                              }).ToList<UserResult>();
            return userResult;
        }
    }
}
