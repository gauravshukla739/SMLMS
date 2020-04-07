using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IUserRoleRepository
    {
        void Add(string UserId, string roleName);
        void Remove(string userId);
        IEnumerable<string> GetRoleNamesByUserId(string userId);
        IEnumerable<User> GetUsersByRoleName(string roleName);
        UserRole GetAllByUserId(Guid userId);
        void UpdateDepartment(string userId, string departmentId);
    }
}
