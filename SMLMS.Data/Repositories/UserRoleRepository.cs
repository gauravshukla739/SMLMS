
using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class UserRoleRepository : RepositoryBase, IUserRoleRepository
    {
        public UserRoleRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(string userId, string roleName)
        {
            Execute(
                sql: @"
                    INSERT INTO AspNetUserRoles(UserId, RoleId)
                    SELECT TOP 1 @userId, Id FROM AspNetRoles
                    WHERE NormalizedName = @roleName",
                param: new { userId, roleName }
            );
        }

        public void UpdateDepartment(string userId, string departmentId)
        {
            Execute(
                sql: @"
                    Update AspNetUserRoles
                    set DepartmentId=@departmentId,IsDeleted=0
                    WHERE UserId = @userId and IsDeleted is null",
                param: new { userId, departmentId  }
            );
        }

       

        public IEnumerable<string> GetRoleNamesByUserId(string userId)
        {
            return Query<string>(
                sql: @"
                    SELECT r.[Name]
                    FROM AspNetUserRoles ur INNER JOIN
                        AspNetRoles r ON ur.RoleId = r.Id
                    WHERE ur.UserId = @userId
                ",
                param: new { userId }
            );
        }

        public UserRole GetAllByUserId(Guid userId)
        {
            return QuerySingleOrDefault<UserRole>(
                sql: @"
                    SELECT UserId,RoleId,DepartmentId,IsDeleted
                    FROM AspNetUserRoles 
                    WHERE UserId = @userId and IsDeleted=0
                ",
                param: new { userId }
            );
        }

        public IEnumerable<User> GetUsersByRoleName(string roleName)
        {
            return Query<User>(
                sql: @"
                    SELECT u.*
                    FROM AspNetUserRoles ur INNER JOIN
	                    AspNetRoles r ON ur.RoleId = r.Id INNER JOIN
	                    AspNetUsers u ON ur.UserId = u.Id
                    WHERE r.NormalizedName = @roleName
                ",
                param: new { roleName });
        }

        public void Remove(string userId)
        {
            Execute(
                sql: @"
                    Update AspNetUserRoles
                    set IsDeleted=1
                    WHERE UserId = @userId and IsDeleted=0",
                param: new { userId }
            );
        }
    }
}
