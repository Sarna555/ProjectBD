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
    class UserCtx : IUserCtx
    {

        private String uname;
        public String UName { get; }

        public UserCtx(String un)
        {
            uname = un;
            OpersRoles = new List<String>();
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

        public static bool Login(String uname, String pass, out IUserCtx uc)
        {
            String[] roles;
            uc = null;

            // autentykacja (tu następuje sprawdzenie z tablicą user/pass z Bazy Danych)
            var user = Class1.FindUser(uname, pass);
            if (user.Count != 0)
            {
                roles = user[0].operations.ToArray();
            }
            else
            {
                return false;
            }

            UserCtx ucl = new UserCtx(uname);
            foreach (String s in roles)
                ucl.AddOperRole(s);
            uc = ucl;

            GenericIdentity gi = new GenericIdentity(uname);

            GenericPrincipal gp = new GenericPrincipal(gi, roles);

            // przypisanie kontekstu (żeby działał mechanizm)
            Thread.CurrentPrincipal = gp;
            return true;

        }
    }
}
