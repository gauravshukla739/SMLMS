using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SMLMS.Data.Repositories
{
    internal class TaskRepository : RepositoryBase, ITaskRepository
    {
        public TaskRepository(IDbTransaction transaction)
           : base(transaction)
        { }
        public void Add(Task entity)
        {
            Execute(
                sql: @"
                    INSERT INTO Task (Id,DepartmentId,Title,[Description],Comment,
                    AdminComment,UpdateDate,CreateDate,CreatedBy,
                    UpdatedBy,IsDeleted,EmployeeId)
                    VALUES
                    (@Id,@DepartmentId,@Title,@Description,@Comment,
		            @AdminComment,@UpdateDate,@CreateDate,@CreatedBy,
                    @UpdatedBy,@IsDeleted,@EmployeeId)",
                param: entity
            );
        }

        public IEnumerable<Task> All()
        {
            return Query<Task>(
                sql: "SELECT * FROM Task"
            );
        }

        public Task Find(string key)
        {
            return QuerySingleOrDefault<Task>(
               sql: "SELECT * FROM Task WHERE Id = @key",
               param: new { key }
           );
        }

        public IList<Task> FindByDepartmentId(Guid departmentId)
        {
            return QuerySingleOrDefault<List<Task>>(
              sql: @"SELECT * FROM Task WHERE departmentId = @departmentId",
              param: new { departmentId }
          );
        }

        
        public IList<Task> FindByEmployeeId(Guid employeeId)
        {
            return QuerySingleOrDefault<List<Task>>(
               sql: @"SELECT * FROM Task WHERE EmployeeId = @employeeId",
               param: new { employeeId }
           );
        }

        public void Remove(string key)
        {
            Execute(
                sql: "Update Task set IsDeleted=1 WHERE Id = @key",
                param: new { key }
            );
        }

        public void Update(Task entity)
        {
            
             Execute(
                sql: @"
                    UPDATE Task SET DepartmentId = @DepartmentId, Title = @Title,
                    [Description] = @Description, Comment = @Comment, AdminComment = @AdminComment,
                    UpdateDate = @UpdateDate, CreateDate = @CreateDate, CreatedBy = @CreatedBy,
                    UpdatedBy = @UpdatedBy, IsDeleted = @IsDeleted, EmployeeId = @EmployeeId 
                    where Id = @Id",
                param: entity);
        }
    }
}
