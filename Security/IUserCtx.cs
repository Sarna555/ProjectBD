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

        bool HasRoleRight(string OperRoleName);

        bool HasGroupRight(string GroupName);


        List<string> GetAllRoles();
    }
}
