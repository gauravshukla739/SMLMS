
using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class EmployeeLeaveRepository : RepositoryBase, IEmployeeLeaveRepository
    {
        public EmployeeLeaveRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public void Add(EmpolyeeLeave entity)
        {
            Execute(
                sql: @"
                 NSERT INTO [dbo].[EployeeLeave]
           ([id]
           ,[UserId]
           ,[LeaveTypeId]
           ,[LeaveCount]
           ,[isDeleted]
           ,[createdate]
           ,[createdby])
     VALUES
           (@id
           ,@UserId
           ,@LeaveTypeId
           ,@LeaveCount
           ,@isDeleted
           ,@createdate
           ,@createdby)",
                param: entity
            );
        }

        public IEnumerable<EmpolyeeLeave> All()
        {
            return Query<EmpolyeeLeave>(
                sql: "SELECT * FROM [dbo].[EmpolyeeLeave] where [isDeleted] is not null or [isDeleted] != 1"
            );
        }

        public EmpolyeeLeave Find(string key)
        {
            return QuerySingleOrDefault<EmpolyeeLeave>(
                sql: "SELECT * FROM [dbo].[EmpolyeeLeave] WHERE [] = @key",
                param: new { key }
            );
        }

        public EmpolyeeLeave FindByName(string EmpolyeeLeaveName)
        {
            throw new NotImplementedException();
        }


        public void Remove(string key)
        {
            Execute(
                sql: @"UPDATE [dbo].[EmpolyeeLeave]
                         SET isDeleted = 1
                    WHERE [id] = @key",
                param: new { key }
            );
            //throw new NotImplementedException();
        }

        public object GetEmployeeLeaves(Guid id, Guid deptId)
        {
            string sqlStr = @"select el.Id , el.UserId, el.LeaveTypeId, el.LeaveCount,el.CreateDate, u.FirstName+ ' '+u.LastName as EmpName , lt.Name as LeaveType from [dbo].[EployeeLeave] el 
                        left join AspNetUsers u on el.userId = u.Id
                        left join LeaveType lt on el.leavetypeid = lt.Id
                        where (el.isdeleted is not null or el.isdeleted = 0)";
            if (id != Guid.Empty)
                sqlStr = sqlStr + " and el.userid = @id ";
           return Query<object>(
                sql: sqlStr,
                 param: new { id }
            );
            //throw new NotImplementedException();
        }

       

        public void Update(EmpolyeeLeave entity)
        {
            Execute(
                sql: @"
                UPDATE [dbo].[EployeeLeave]
                   SET [UserId] = @UserId
                      ,[LeaveTypeId] = @LeaveTypeId
                      ,[LeaveCount] = @LeaveCount
                      ,[updatedate] = @updatedate
                      ,[updatedby] = @updatedby
                 WHERE [Id] = @Id
                     ",
                param: entity
            );
        }
    }
}
