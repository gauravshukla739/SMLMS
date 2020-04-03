using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IDepartmentService
    {
        Task<ServiceResponse> CreateOrUpdate(DepartmentDto Name,ClaimsPrincipal claims);
        Task<ServiceResponse> Delete(Guid Id,ClaimsPrincipal claims);
        Task<ServiceResponse> GetById(Guid Id);
        Task<ServiceResponse> All();

    }
}
