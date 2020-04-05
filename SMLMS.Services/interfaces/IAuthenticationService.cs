
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IAuthenticationService
    {
         Task<ServiceResponse> SignIn(UserDto user);

        Task<ServiceResponse> CreateUser(UserDto _user,ClaimsPrincipal claims);

        Task<ServiceResponse> CreateRole(string roleName);
       // object GetClaimsValue(string type);
        Task<ServiceResponse> Logout();
        Task<ServiceResponse> Update(UserDto user,ClaimsPrincipal claims);
        Task<ServiceResponse> Detail(string id);
    }
}
