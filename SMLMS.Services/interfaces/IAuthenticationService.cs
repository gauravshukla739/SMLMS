
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IAuthenticationService
    {
         Task<ServiceResponse> SignIn(UserDto user);

        Task<ServiceResponse> CreateUser(UserDto _user);

        Task<ServiceResponse> CreateRole(string roleName);
    }
}
