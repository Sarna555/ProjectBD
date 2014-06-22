using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Principal;
using Logic;

namespace Security
{
    public class UserCtx : IUserCtx
    {

        public String uname { get; private set; }
        public List<string> OpersRoles;

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
        /// <param name="un"></param>
        /// <param name="OpersRole"></param>
        public UserCtx(String un, List<String> OpersRole)
        {
            uname = un;
            this.OpersRoles = OpersRoles;
        }

        public void AddOperRole(String OperRoleName)
        {
            OpersRoles.Add(OperRoleName);
        }

        public bool HasRoleRight(String OperRoleName)
        {
            throw new NotImplementedException();
        }

        public bool HasGroupRight(String GroupName)
        {
            throw new NotImplementedException();
        }

        public List<String> GetAllRoles()
        {
            throw new NotImplementedException();
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
        /// <param name="uname"></param>
        /// <param name="pass"></param>
        /// <param name="uc"></param>
        /// <returns></returns>
        public static bool Login(String uname, String pass, out IUserCtx uc)
        {
            List<string> roles;
            uc = null;

            // autentykacja (tu następuje sprawdzenie z tablicą user/pass z Bazy Danych)
            var user = Class1.FindUser(uname, pass);
            if (user.Count != 0)
            {
                roles = user[0].operations;
            }
            else
            {
                return false;
            }

            uc = new UserCtx(uname, roles);

            GenericIdentity gi = new GenericIdentity(uname);

            GenericPrincipal gp = new GenericPrincipal(gi, roles.ToArray());

            // przypisanie kontekstu (żeby działał mechanizm)
            Thread.CurrentPrincipal = gp;
            return true;

        }
    }
}
