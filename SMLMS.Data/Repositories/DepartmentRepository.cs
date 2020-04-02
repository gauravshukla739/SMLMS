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
                   INSERT INTO [dbo].[Department] ([Name],[RoleId],[CreatedBy])
                    VALUES(@Name, @RoleId,@CreatedBy)",
                param: entity
            );
            
        }

        public IEnumerable<DepartmentDto> All()
        {
            return Query<DepartmentDto>(
                sql: "SELECT Name,Id FROM [dbo].[Department]"
            );
        }

        public DepartmentDto Find(string key)
        {
            throw new NotImplementedException();
        }

        public DepartmentDto FindById(Guid key)
        {
            return QuerySingleOrDefault<DepartmentDto>(
                sql: "SELECT Name FROM [dbo].[Department]  WHERE Id = @key",
                param: new { key }
            );
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid key, string userName)
        {
            Execute(
                sql: @"UPDATE [dbo].[Department]
                         SET IsDeleted = 1,IsDeletedBy=@userName
                    WHERE [Id] = @key",
                param: new { key,userName }
            );
        }

        public void Update(DepartmentDto entity)
        {
            Execute(
                sql: @"
                    UPDATE [dbo].[Department]
                       SET 
                        [Name]=@Name
                      ,[updatedate] = @UpdateDate
                      ,[updatedby] = @UpdatedBy
                    WHERE Id = @Id",
                param: entity
            );
        }

    }
}
