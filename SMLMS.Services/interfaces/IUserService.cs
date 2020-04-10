
using Microsoft.AspNetCore.Http;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
        Task<ServiceResponse> Import(List<UserDto> user, ClaimsPrincipal claims);

        Task<ServiceResponse> UploadImage(IFormFile file, ClaimsPrincipal claims);
        Task<ServiceResponse> Promote(UserDto user, ClaimsPrincipal claims);


    }
}
