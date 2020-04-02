using System;
using System.Collections.Generic;
using System.Text;
using SMLMS.Model.Core;

namespace SMLMS.Data.Interfaces
{
    public interface ITaskRepository : IRepository<Task, string>
    {
        IList<Task> FindByEmployeeId(Guid employeeId);
        IList<Task> FindByDepartmentId(Guid departmentId);
    }
}
