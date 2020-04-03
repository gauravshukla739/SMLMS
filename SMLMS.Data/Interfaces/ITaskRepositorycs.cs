using System;
using System.Collections.Generic;
using System.Text;
using SMLMS.Model.Core;

namespace SMLMS.Data.Interfaces
{
    public interface ITaskRepository : IRepository<Task, string>
    {
        IEnumerable<Task> FindByEmployeeId(Guid employeeId);
        IEnumerable<Task> FindByDepartmentId(Guid departmentId);
    }
}
