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



        public IEnumerable<EmployeeAttendanceModel> All()
        {
            return Query<EmployeeAttendanceModel>(
                sql: "select b.FirstName, b.LastName,a.SignIn,a.SignOut, a.CreatedOn  ,(SELECT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND,a.SignIn, a.SignOut),0), 108) as ElapsedTime) as TotalTime from [dbo].[Attendance] as a join [dbo].[AspNetUsers] b on a.UserId =b.Id"
            );
        }

        public Attendance FindUserById(string UserId)
        {
            var query = "SELECT TOP (1) [Id], [UserId], [SignIn],[SignOut],[CreatedOn],[UpdatedOn],[CreatedBy] ,[UpdatedBy],[IsDeletedBy],[IsDeleted] FROM Attendance WHERE UserId=@UserId and cast(CreatedOn as Date) = cast(getdate() as Date) order by CreatedOn desc";
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

        public IEnumerable<Attendance> EmployeeAttendance_DateFilter(string sDate, string eDate)
        {
            return Query<Attendance>(
                sql: "SELECT * from [dbo].[Attendance] WHERE CreatedOn BETWEEN '2020-04-02' AND '2020-04-02 23:59:59.997'"
            );
        }

        public void Update(AttendanceDto entity)
        {
            Execute(
                sql: @"
                    UPDATE Attendance SET [SignOut] = @SignOut, 
                           [UpdatedBy] = @UpdatedBy ,[UpdatedOn]=@UpdatedOn
                            WHERE Id = @Id",
                param: entity
            );
        }

        
    }
}
