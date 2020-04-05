
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IRoleService
    {
        Task<ServiceResponse> GetAllRoles();
        Task<ServiceResponse> SaveUpdateRole(RoleDto _role, ClaimsPrincipal claims);
        Task<ServiceResponse> DeleteRole(string roleId);
        Task<ServiceResponse> GetRoleById(string roleId);
        Task<ServiceResponse> GetAllModule();
        Task<ServiceResponse> SaveUpdateModule(ModuleDto model);
        Task<ServiceResponse> DeleteModule(string id);
        Task<ServiceResponse> GetModuleById(string id);
        Task<ServiceResponse> GetAllRoleModulePermission();
        Task<ServiceResponse> SaveUpdateRoleModulePermission(RoleModulePermissionDto model);
        Task<ServiceResponse> DeleteRoleModulePermission(string id);
        Task<ServiceResponse> GetRoleModulePermissionById(string id);
    }
}
