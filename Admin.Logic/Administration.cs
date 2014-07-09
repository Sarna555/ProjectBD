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
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all users</returns>
        //[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
        public static List<UserResult> GetAllUsers()
        {

            var db = new SQLtoLinqDataContext();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static List<string> GetAllUserOperations(string login)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from o in db.operations
                              from u2o in db.users2operations
                              from u in db.Users
                              where o.operation_ID == u2o.operation_ID && u.user_ID == u2o.user_ID && u.user_ID == ele.user_ID
                              select o.name)
                                .Union
                                (from o in db.operations
                                 from g2o in db.groups2operations
                                 from u2g in db.users2groups
                                 from u in db.Users
                                 from g in db.groups
                                 where u.user_ID == u2g.user_ID && g.group_ID == u2g.group_ID && g.group_ID == g2o.group_ID && o.operation_ID == g2o.operation_ID && u.user_ID == ele.user_ID
                                 select o.name).ToList<string>();
            return result;
            //Nic nie poradze, jak na razie optymalniej nie chce działać :D
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
        public static List<string> GetAllGroups()
        {
            var db = new SQLtoLinqDataContext();
            var result = (from g in db.groups
                          select g.name).ToList<string>();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
        public static List<string> GetGroupOperations(string name)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from g in db.groups
                          where g.name == name
                          select (from o in db.operations
                                  from g2o in db.groups2operations
                                  where g.group_ID == g2o.group_ID && g2o.operation_ID == o.operation_ID
                                  select o.name).ToList<string>()).ToArray<List<string>>();
            return result[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>user operations</returns>
        //[PrincipalPermissionAttribute(SecurityAction.Demand, Role = "ReadUsers")]
        public static List<string> GetUserOperations(string login)
        {
            var db = new SQLtoLinqDataContext();
            var result = (from u in db.Users
                          where u.login == login
                          select (from o in db.operations
                                  from u2o in db.users2operations
                                  where u.user_ID == u2o.user_ID && u2o.operation_ID == o.operation_ID
                                  select o.name).ToList<string>()).ToArray<List<string>>();
            return result[0];
        }
    }
}
