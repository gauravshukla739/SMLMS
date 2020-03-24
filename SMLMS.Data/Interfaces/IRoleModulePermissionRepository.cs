
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IRoleModulePermissionRepository : IRepository<RoleModulePermission, string>
    {
        //Role FindByName(string roleName);
    }
}
