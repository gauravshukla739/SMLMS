
using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class RoleModulePermissionRepository : RepositoryBase, IRoleModulePermissionRepository
    {
        public RoleModulePermissionRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public void Add(RoleModulePermission entity)
        {
            Execute(
                sql: @"
                   INSERT INTO [dbo].[RoleModulePermission] ([id],[roleid],[moduleid],[canadd],[canedit],
                    [candelete],[canview],[isDeleted],[createdate],[updatedate],[createdby],[updatedby])
                    VALUES(@Id, @RoleId,@ModuleId ,@CanAdd ,@CanEdit ,@CanDelete,@CanView,
                    @IsDeleted, @CreateDate , @UpdateDate,@CreatedBy,@UpdatedBy)",
                param: entity
            );
        }

        public IEnumerable<RoleModulePermission> All()
        {
            return Query<RoleModulePermission>(
                sql: "SELECT * FROM [dbo].[RoleModulePermission]"
            );
        }

        public RoleModulePermission Find(string key)
        {
            return QuerySingleOrDefault<RoleModulePermission>(
                sql: "SELECT * FROM [dbo].[RoleModulePermission] WHERE Id = @key",
                param: new { key }
            );
        }

        //public RoleModulePermission FindByName(string roleName)
        //{
        //    return QuerySingleOrDefault<RoleModulePermission>(
        //        sql: "SELECT * FROM AspNetRoles WHERE [Name] = @roleName",
        //        param: new { roleName }
        //    );
        //}


        public void Remove(string key)
        {
            Execute(
                sql: @"UPDATE [dbo].[RoleModulePermission]
                         SET 
                      ,[name] = @name
                    WHERE isDeleted = @IsDeleted",
                param: new { key }
            );

            throw new NotImplementedException();
        }

        public void Update(RoleModulePermission entity)
        {
            Execute(
                sql: @"
                    UPDATE [dbo].[RoleModulePermission]
                       SET 
                      ,[roleid] = @RoleId
                      ,[moduleid] = @ModuleId
                      ,[canadd] = @CanAdd
                      ,[canedit] = @CanEdit
                      ,[candelete] = @CanDelete
                      ,[canview] = @CanView
                      ,[isDeleted] = @IsDeleted
                      ,[createdate] = @CreateDate
                      ,[updatedate] = @UpdateDate
                      ,[createdby] = @CreatedBy
                      ,[updatedby] = @UpdatedBy
                    WHERE Id = @Id",
                param: entity
            );
        }
    }
}
