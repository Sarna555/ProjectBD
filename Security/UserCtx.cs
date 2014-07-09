using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Principal;

namespace Security
{
    public class UserCtx : IUserCtx
    {

        public String uname { get; private set; }
        public List<string> OpersRoles { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="un"></param>
        public UserCtx(String un)
        {
            uname = un;
            OpersRoles = new List<String>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="opersRole"></param>
        public UserCtx(String uname, List<String> opersRole)
        {
            this.uname = uname;
            this.OpersRoles = opersRole;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperRoleName"></param>
        /// <returns></returns>
        public bool HasRoleRight(String OperRoleName)
        {
            return this.OpersRoles.Contains(OperRoleName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public bool HasGroupRight(String GroupName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> GetAllRoles()
        {
            return OpersRoles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uc"></param>
        public static void Logout(ref IUserCtx uc)
        {
            // IPrincipal  gp ;
            //gp = Thread.CurrentPrincipal;

            uc = null;
            Thread.CurrentPrincipal = null;
            //IIdentity id = gp.Identity;
            //id = null;
            //gp = null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uname">login</param>
        /// <param name="pass">password</param>
        /// <param name="uc">User Context</param>
        /// <returns>If user exists returns true</returns>
        public static bool Login(String login, String pass, out IUserCtx uc)
        {
            uc = null;
            var db = new SQLtoLinqDataContext();

            // autentykacja (tu następuje sprawdzenie z tablicą user/pass z Bazy Danych)
            /*roles = (from u in db.Users
                     from o in db.operations
                     where )*/
            var result = (from p in db.Users
                          where p.login == login && p.password == pass
                          select new UserResult
                          {
                              user_ID = p.user_ID,
                              name = p.name,
                              surname = p.surname,
                              login = p.login
                          }).ToArray<UserResult>();
            if (result.Length != 1)
                return false;

            var roles = (from o in db.operations
                         from u2o in db.users2operations
                         from u in db.Users
                         where o.operation_ID == u2o.operation_ID && u.user_ID == u2o.user_ID && u.user_ID == result[0].user_ID
                         select o.name)
                         .Union
                         (from o in db.operations
                          from g2o in db.groups2operations
                          from u2g in db.users2groups
                          from u in db.Users
                          from g in db.groups
                          where u.user_ID == u2g.user_ID && g.group_ID == u2g.group_ID && g.group_ID == g2o.group_ID && o.operation_ID == g2o.operation_ID && u.user_ID == result[0].user_ID
                          select o.name).ToList<string>();
            //tak bardzo brzydko ;(
            uc = new UserCtx(login, roles);

            GenericIdentity gi = new GenericIdentity(login);

            GenericPrincipal gp = new GenericPrincipal(gi, roles.ToArray());

            // przypisanie kontekstu (żeby działał mechanizm)
            Thread.CurrentPrincipal = gp;
            return true;

        }

        public UserResult FindUser(string login, string pass, ref SQLtoLinqDataContext db)
        {
            
            return null;
        }
    }
}
