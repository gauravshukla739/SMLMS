using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class AttendanceRepository : RepositoryBase, IAttendanceRepository
    {
        public AttendanceRepository(IDbTransaction transaction)
           : base(transaction)
        { }

        public void Add(AttendanceDto entity)
        {
            Execute(
        sql: @"
                    INSERT INTO [dbo].[Attendance]([Id],[UserId],[SignIn],
	                    [CreatedOn],  [CreatedBy],
	                   [IsDeleted])
                    VALUES(@Id,@UserId, @SignIn, @CreatedOn,
	                    @CreatedBy, @IsDeleted)",
        param: entity);
        }



        public IEnumerable<Attendance> All()
        {
            return Query<Attendance>(
                sql: "SELECT * FROM [dbo].[Attendance]"
            );
        }

        public Attendance FindUserById(string UserId)
        {
            var query = "SELECT TOP (1) [UserId], [SignIn],[SignOut],[CreatedOn],[UpdatedOn],[CreatedBy] ,[UpdatedBy],[IsDeletedBy],[IsDeleted] FROM Attendance WHERE UserId=@UserId and cast(CreatedOn as Date) = cast(getdate() as Date) order by CreatedOn desc";
            var data = QuerySingleOrDefault<Attendance>(sql: query, param: new { UserId });
            return data;
        }

        public User Find(string key)
        {
            return QuerySingleOrDefault<User>(
                sql: "SELECT * FROM Attendance WHERE Id = @key",
                param: new { key }
            );
        }

        public IEnumerable<Attendance> EmployeeAttendance_DateFilter(DateTime? startDate, DateTime? endDate)
        {
            return Query<Attendance>(
                sql: "SELECT * from [dbo].[Attendance] WHERE CreatedOn BETWEEN @startDate AND @endDate"
            );
        }

        public void Update(AttendanceDto entity)
        {
            Execute(
                sql: @"
                    UPDATE Attendance SET SignOut = @SignOut, 
                            UpdatedOn = @UpdatedOn, UpdatedBy = @UpdatedBy 
                            WHERE Id = @Id",
                param: entity
            );
        }

        
    }
}
