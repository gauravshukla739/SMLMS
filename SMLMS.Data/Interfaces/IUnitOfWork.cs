using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILeaveRepositry LeaveRepositry { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        IRoleRepository RoleRepository { get; }
        IModuleRepository ModuleRepository { get; }
        IRoleModulePermissionRepository RoleModulePermissionRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ITaskRepository TaskRepository { get; }
        IEmployeeLeaveRepository EmployeeLeaveRepository { get; }

        void Commit();
    }
}
