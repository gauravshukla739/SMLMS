
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface ITaskService
    {
        Task<ServiceResponse> GetAllTasks();
        Task<ServiceResponse> SaveUpdateTask(SMLMS.Model.Core.Task _task, ClaimsPrincipal claims);
        Task<ServiceResponse> DeleteTask(Guid taskId, ClaimsPrincipal claims);
        Task<ServiceResponse> GetTaskById(string taskId);
        Task<ServiceResponse> GetTaskByEmployeeId(Guid id);
        Task<ServiceResponse> GetMyTaskByEmployeeId(Guid id);        
        Task<ServiceResponse> GetTaskByDepartmentId(Guid id);
    }
}
