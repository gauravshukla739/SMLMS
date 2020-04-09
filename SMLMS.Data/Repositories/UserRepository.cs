
using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public void Add(ApplicationUser entity)
        {
            Execute(
                sql: @"
                    INSERT INTO AspNetUsers(Id, AccessFailedCount, ConcurrencyStamp, Email,
	                    EmailConfirmed, LockoutEnabled, LockoutEnd, NormalizedEmail,
	                    NormalizedUserName, PasswordHash, PhoneNumber, PhoneNumberConfirmed,
	                    SecurityStamp, TwoFactorEnabled, UserName)
                    VALUES(@Id, @AccessFailedCount, @ConcurrencyStamp, @Email, @EmailConfirmed,
	                    @LockoutEnabled, @LockoutEnd, @NormalizedEmail, @NormalizedUserName,
	                    @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp,
	                    @TwoFactorEnabled, @UserName)",
                param: entity
            );
        }

        public void AddRole(UserRole entity)
        {
            Execute(
                sql: @"
                    INSERT INTO [dbo].[AspNetUserRoles]
                           ([UserId]
                           ,[RoleId])
                     VALUES
                           (@UserId
                           ,@RoleId)",
                param: entity
            );
        }

        public UserRole GetRole(string key)
        {
            return QuerySingleOrDefault<UserRole>(
                sql: "SELECT * FROM AspNetUserRoles WHERE UserId = @key",
                param: new { key }
            );
        }
        public int Delete(string key)
        {
            return ExecuteScalar<int>(
                sql: "update [AspNetUsers] set [IsDeleted] = 1 where Id = @key",
                param: new { key }
            );
        }

        public IEnumerable<ApplicationUser> All()
        {
            return Query<ApplicationUser>(
                sql: "SELECT *,role.DepartmentId FROM AspNetUsers as userd INNER JOIN AspNetUserRoles as role ON userd.Id=role.UserId where (userd.[IsDeleted] is null or userd.[IsDeleted] = 0) and role.isdeleted=0"
            );
        }

        public IEnumerable<UserDepartmentListDto> GetAll()
        {
            return Query<UserDepartmentListDto>(
                sql: "SELECT *,role.UserId,role.DepartmentId,role.RoleId,dept.Name as DepartmentName FROM AspNetUsers as userd INNER JOIN AspNetUserRoles as role ON userd.Id=role.UserId  INNER JOIN Department as dept ON role.DepartmentId=dept.Id  where (userd.[IsDeleted] is null or userd.[IsDeleted] = 0) and role.isdeleted=0 and dept.isdeleted=0"
            );
        }

        public void UpdateImage(string userId, byte[] image, string updatedBy)
        {
            var updateDate = DateTime.Now;
            Execute(
                sql: @"
                    Update AspNetUsers
                    set Image=@image,UpdatedBy=@updatedBy,UpdateDate=@updateDate
                    WHERE Id = @userId and (IsDeleted=0 or IsDeleted is null)",
                param: new { userId, image,updatedBy,updateDate }
            );
        }

        public ApplicationUser Find(string key)
        {
            return QuerySingleOrDefault<ApplicationUser>(
                sql: "SELECT * FROM AspNetUsers WHERE Id = @key",
                param: new { key }
            );
        }

        public ApplicationUser FindByNormalizedEmail(string normalizedEmail)
        {
            return QuerySingleOrDefault<ApplicationUser>(
                sql: "SELECT * FROM AspNetUsers WHERE NormalizedEmail = @normalizedEmail",
                param: new { normalizedEmail }
            );
        }

        public ApplicationUser FindByNormalizedUserName(string normalizedUserName)
        {
            return QuerySingleOrDefault<ApplicationUser>(
                sql: "SELECT * FROM AspNetUsers WHERE NormalizedUserName = @normalizedUserName",
                param: new { normalizedUserName }
            );
        }

        public void Remove(string key)
        {
            Execute(
                sql: "DELETE FROM AspNetUsers WHERE Id = @key",
                param: new { key }
            );
        }

        public void Update(ApplicationUser entity)
        {
            Execute(
                sql: @"
                    UPDATE AspNetUsers SET AccessFailedCount = @AccessFailedCount,
	                    ConcurrencyStamp = @ConcurrencyStamp, Email = @Email,
	                    EmailConfirmed = @EmailConfirmed, LockoutEnabled = @LockoutEnabled,
	                    LockoutEnd = @LockoutEnd, NormalizedEmail = @NormalizedEmail,
	                    NormalizedUserName = @NormalizedUserName, PasswordHash = @PasswordHash,
	                    PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed,
	                    SecurityStamp = @SecurityStamp, TwoFactorEnabled = @TwoFactorEnabled,
	                    UserName = @UserName
                    WHERE Id = @Id",
                param: entity);
        }
    }
}
