
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface ITaskService
    {
        Task<ServiceResponse> GetAllTasks();
        Task<ServiceResponse> SaveUpdateTask(SMLMS.Model.Core.Task _task);
        Task<ServiceResponse> DeleteTask(string taskId);
        Task<ServiceResponse> GetTaskById(string taskId);
        Task<ServiceResponse> GetTaskByEmployeeId(Guid id);
        Task<ServiceResponse> GetTaskByDepartmentId(Guid id);
    }
}
