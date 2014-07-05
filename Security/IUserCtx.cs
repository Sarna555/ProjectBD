using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public interface IUserCtx
    {
        String uname { get; }

        bool HasRoleRight(String OperRoleName);

        bool HasGroupRight(String GroupName);

        bool Login(String uname, String pass, out IUserCtx uc);

        void Logout(ref IUserCtx uc);

        List<String> GetAllRoles();
    }
}
