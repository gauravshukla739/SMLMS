using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        IRoleRepository RoleRepository { get; }
        IModuleRepository ModuleRepository { get; }
        IRoleModulePermissionRepository RoleModulePermissionRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        void Commit();
    }
}
