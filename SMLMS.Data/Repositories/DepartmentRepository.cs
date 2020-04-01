using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class DepartmentRepository :RepositoryBase , IDepartmentRepository
    {
        public DepartmentRepository(IDbTransaction transaction)
              : base(transaction)
        { }

        public void Add(DepartmentDto entity)
        {
            Execute(
                sql: @"
                   INSERT INTO [dbo].[Department] ([Name],[RoleName])
                    VALUES(@Name, @RoleName)",
                param: new { entity.Name,entity.RoleName}
            );
            
        }

        public IEnumerable<DepartmentDto> All()
        {
            return Query<DepartmentDto>(
                sql: "SELECT Name,RoleName FROM [dbo].[Department]"
            );
        }

        //public RoleModulePermission Find(string key)
        //{
        //    return QuerySingleOrDefault<RoleModulePermission>(
        //        sql: "SELECT * FROM [dbo].[RoleModulePermission] WHERE Id = @key",
        //        param: new { key }
        //    );
        //}


        //public void Remove(string key,string userName)
        //{
        //    Execute(
        //        sql: @"UPDATE [dbo].[Department]
        //                 SET IsDeleted = 1 and IsDeletedBy=@IsDeletedBy
        //            WHERE [Name] = @Name",
        //        param: new { key }
        //    );
        //}

        //public void Update(RoleModulePermission entity)
        //{
        //    Execute(
        //        sql: @"
        //            UPDATE [dbo].[Department]
        //               SET 
        //                [Name]=@Name
        //              ,[updatedate] = @UpdateDate
        //              ,[updatedby] = @UpdatedBy
        //            WHERE Id = @Id",
        //        param: entity
        //    );
        //}

    }
}
