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
    public interface ILeaveService
    {
        Task<ServiceResponse> GetLeaveType();
        Task<ServiceResponse> PostLeave(LeaveDto model);
        Task<ServiceResponse> DeleteLeave(string id);
        Task<ServiceResponse> RequestLeave(RequestLeave model, ClaimsPrincipal claim);
        Task<ServiceResponse> GetLeaveRequest(ClaimsPrincipal claim);
        Task<ServiceResponse> DeleteLeaveRequest(string id);

        Task<ServiceResponse> GetDataBasedOnId(ClaimsPrincipal claim);

        Task<ServiceResponse> ApproveLeaveRequest(Guid id, ClaimsPrincipal claim);

        Task<ServiceResponse> GetEmployeeLeaves(Guid id, Guid deptId);
        Task<ServiceResponse> SaveUpdateEmployeeLeave(EmpolyeeLeaveDto model);
        Task<ServiceResponse> DeleteEmployeeLeave(Guid id);

    }
}
