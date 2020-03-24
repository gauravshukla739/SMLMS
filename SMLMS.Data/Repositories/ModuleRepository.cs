
using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class ModuleRepository : RepositoryBase, IModuleRepository
    {
        public ModuleRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public void Add(Module entity)
        {
            Execute(
                sql: @"
                    INSERT INTO [dbo].[Module] ([id],[name],[displayname],[description],
                     [isDeleted],[createdate],[updatedate],[createdby],[updatedby])
                    VALUES(@Id, @Name, @DisplayName, @Description , @IsDeleted,
                      @CreateDate , @UpdateDate,@CreatedBy,@UpdatedBy )",
                param: entity
            );
        }

        public IEnumerable<Module> All()
        {
            return Query<Module>(
                sql: "SELECT * FROM [dbo].[Module]"
            );
        }

        public Module Find(string key)
        {
            return QuerySingleOrDefault<Module>(
                sql: "SELECT * FROM [dbo].[Module] WHERE Id = @key",
                param: new { key }
            );
        }

        public Module FindByName(string moduleName)
        {
            return QuerySingleOrDefault<Module>(
                sql: "SELECT * FROM [dbo].[Module] WHERE [Name] = @moduleName",
                param: new { moduleName }
            );
        }


        public void Remove(string key)
        {
            Execute(
                sql: @"UPDATE [dbo].[Module]
                         SET 
                      ,[name] = @name
                    WHERE isDeleted = @IsDeleted",
                param: new { key }
            );

            throw new NotImplementedException();
        }

        public void Update(Module entity)
        {
            Execute(
                sql: @"
                      UPDATE [dbo].[Module]
                         SET 
                      ,[name] = @name
                      ,[displayname] = @DisplayName
                      ,[description] = @Description
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
