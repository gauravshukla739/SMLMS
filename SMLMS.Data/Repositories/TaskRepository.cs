using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

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
                    UpdatedBy,IsDeleted,EmployeeId,AssignTo,Status,EstimatedDate)
                    VALUES
                    (@Id,@DepartmentId,@Title,@Description,@Comment,
		            @AdminComment,@UpdateDate,@CreateDate,@CreatedBy,
                    @UpdatedBy,@IsDeleted,@EmployeeId,@AssignTo,@Status,@EstimatedDate)",
                param: entity
            );
        }

        public IEnumerable<Task> All()
        {
            return Query<Task>(
                sql: "SELECT * FROM Task"
            );
        }
        public IEnumerable<TaskDto> AllTask()
        {
            return Query<TaskDto>(
                sql: @"SELECT t.[Id],t.[DepartmentId],t.[Title],t.[Description],t.[Comment]
                   , t.[AdminComment], t.[UpdateDate], t.[CreateDate], t.[CreatedBy]
                   , t.[UpdatedBy], t.[IsDeleted], t.[EmployeeId], t.[DeletedBy]
                   , t.[Status], t.[EstimatedDate], t.[AssignTo], u.[FirstName] + ' ' + u.[LastName] as [AssignToName]
                    FROM Task t join AspNetUsers u on t.AssignTo = u.Id
                    WHERE t.[IsDeleted] is null or t.[IsDeleted] = 0"
            );
        }
        public Task Find(string key)
        {
            return QuerySingleOrDefault<Task>(
               sql: "SELECT * FROM Task WHERE Id = @key",
               param: new { key }
           );
        }

        public IEnumerable<Task> FindByDepartmentId(Guid departmentId)
        {
            return Query<Task>(
              sql: @"SELECT * FROM Task WHERE departmentId = @departmentId",
              param: new { departmentId }
          );
        }

        
        public IEnumerable<TaskDto> FindByEmployeeId(Guid employeeId)
        { 
          return Query<TaskDto>(
               sql: @"SELECT t.[Id],t.[DepartmentId],t.[Title],t.[Description],t.[Comment]
                   ,t.[AdminComment],t.[UpdateDate],t.[CreateDate],t.[CreatedBy]
                   ,t.[UpdatedBy],t.[IsDeleted],t.[EmployeeId],t.[DeletedBy]
                   ,t.[Status],t.[EstimatedDate],t.[AssignTo],u.[FirstName]+' '+ u.[LastName] as [AssignToName]
                    FROM Task t join AspNetUsers u on t.AssignTo=u.Id
                    WHERE t.EmployeeId = @employeeId and ( t.[IsDeleted] is null or t.[IsDeleted] = 0 )",
               param: new { employeeId }
           );
          
        }
        public IEnumerable<TaskDto> FindByAssignToId(Guid assignTo)
        {
            return Query<TaskDto>(
                 sql: @"SELECT t.[Id],t.[DepartmentId],t.[Title],t.[Description],t.[Comment]
                   ,t.[AdminComment],t.[UpdateDate],t.[CreateDate],t.[CreatedBy]
                   ,t.[UpdatedBy],t.[IsDeleted],t.[EmployeeId],t.[DeletedBy]
                   ,t.[Status],t.[EstimatedDate],t.[AssignTo],u.[FirstName]+' '+ u.[LastName] as [AssignToName]
                    FROM Task t join AspNetUsers u on t.AssignTo=u.Id
                    WHERE t.AssignTo = @assignTo and ( t.[IsDeleted] is null or t.[IsDeleted] = 0 )",
                 param: new { assignTo }
             );

        }
        
        public void Remove(string key)
        {
            Execute(
                sql: "Update Task set IsDeleted=1 WHERE Id = @key",
                param: new { key }
            );
        }

        public void Remove(Guid key, string userName)
        {
            Execute(
                 sql: "Update Task set IsDeleted=1,DeletedBy=@userName WHERE Id = @key",
                 param: new { key, userName }
             );

        }

        public void Update(Task entity)
        {
            
             Execute(
                sql: @"
                    UPDATE Task SET DepartmentId = @DepartmentId, Title = @Title,
                    [Description] = @Description, Comment = @Comment, AdminComment = @AdminComment,
                    UpdateDate = @UpdateDate, CreateDate = @CreateDate, CreatedBy = @CreatedBy,
                    UpdatedBy = @UpdatedBy, IsDeleted = @IsDeleted, EmployeeId = @EmployeeId, 
                    AssignTo = @AssignTo, Status = @Status, EstimatedDate = @EstimatedDate
                    where Id = @Id",
                param: entity);
        }
    }
}
