
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IUserService
    {
         Task<ServiceResponse> GetAll();
        Task<ServiceResponse> AddUserRole(UserRoleDto userRole);

        Task<ServiceResponse> GetRole(string userId);

        Task<ServiceResponse> Delete(string userId);

    }
}
