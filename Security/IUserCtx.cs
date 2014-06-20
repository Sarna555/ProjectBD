using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    interface IUserCtx
    {
        String UName { get; }

        bool HasRoleRight(String OperRoleName);

        bool HasGroupRight(String GroupName);

        List<String> GetAllRoles();
    }
}
