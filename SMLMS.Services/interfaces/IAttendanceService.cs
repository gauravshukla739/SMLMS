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

        Task<ServiceResponse> GetEmployeAttendance(string userid,string role, string month, string dept, string user);
        Task<ServiceResponse> GetPresent_AbsentDays_Emp(string userid);

        Task<ServiceResponse> FindByEmail(string Email);

        Task<ServiceResponse> GetPresent_AbsentDays_Emp(string userid, string role);

        Task<ServiceResponse> getAllEmployess();
        Task<ServiceResponse> GetTodayPunchIn();
        Task<ServiceResponse> GetEmployeeatendanceDetails(string userid);

    }
}
