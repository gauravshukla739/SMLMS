
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IRoleModulePermissionRepository : IRepository<RoleModulePermission, string>
    {
        //Role FindByName(string roleName);
         void AddRoleToTask(List<RoleTaskPermissionDto> entity);
         IEnumerable<RoleTaskPermissionDto> FindPermissionByRole(string key);
         IEnumerable<RoleTaskPermissionDto> GetAllPermission();
        void Truncate();

    }
}
