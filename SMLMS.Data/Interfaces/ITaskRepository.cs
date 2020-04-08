using System;
using System.Collections.Generic;
using System.Text;
using SMLMS.Model.Core;

namespace SMLMS.Data.Interfaces
{
    public interface ITaskRepository : IRepository<Task, string>
    {
        IEnumerable<TaskDto> FindByEmployeeId(Guid employeeId);
        IEnumerable<TaskDto> FindByAssignToId(Guid employeeId);        
        IEnumerable<TaskDto> FindByDepartmentId(Guid departmentId);
        IEnumerable<TaskDto> AllTask();
        void Remove(Guid key, string userName);
    }
}
