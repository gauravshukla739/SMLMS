using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IAttendanceService
    {
        Task<ServiceResponse> CreateOrUpdate(ClaimsPrincipal claims);
        Task<ServiceResponse> Delete(string Name);
        Task<ServiceResponse> GetAll();

        Task<ServiceResponse> Employess(AttendanceDto model);

        Task<ServiceResponse> GetEmployeAttendance(string userid);



    }
}
