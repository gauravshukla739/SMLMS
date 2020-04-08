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



        public IEnumerable<EmployeeAttendanceModel> All(string month, string dept, string email,int workingDays)
        {
            //var query = @"select b.FirstName, b.LastName,a.SignIn,a.SignOut, a.CreatedOn  ," +
            //    "(SELECT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND,a.SignIn, a.SignOut),0), 108) as ElapsedTime) as TotalTime," +
            //    "(SELECT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND,a.SignIn, (SELECT GETDATE())),0), 108) as ElapsedTime) as ResumeTime" +
            //    " from [dbo].[Attendance] as a join [dbo].[AspNetUsers] b on a.UserId =b.Id " +
            //    "join [dbo].[AspNetUserRoles] c on b.Id =c.UserId";
            //if (month != null && dept == null && email == null)
            //{
            //    query = query + " Where MONTH(a.CreatedOn) = @month";
            //}
            //else if (month != null && dept != null && email == null)
            //{
            //    query = query + " Where MONTH(a.CreatedOn) = @month and c.DepartmentId =@dept";
            //}
            //else if (month != null && dept != null && email != null)
            //{
            //    query = query + " Where MONTH(a.CreatedOn) = @month and c.DepartmentId =@dept and b.NormalizedEmail =@email";
            //}

            //var data = Query<EmployeeAttendanceModel>(sql: query, param: new { month, dept, email });

            // ----To check User Status (Present or absent)

            var sqlQuery = @"select count(a.UserId)as PresentDays,a.UserId,b.FirstName,b.LastName,(d.Name) as DepartmentName from [dbo].[Attendance] as a
                         join [dbo].[AspNetUsers] b on a.UserId =b.Id 
                           join [dbo].[AspNetUserRoles] c on b.Id =c.UserId
                           join [dbo].[Department] d on c.DepartmentId =d.Id";
            if (month != null && dept == null && email == null)
            {
                sqlQuery = sqlQuery + " Where MONTH(a.CreatedOn) = @month group by a.UserId, b.FirstName,b.LastName,d.Name";
            }
            else if (month != null && dept != null && email == null)
            {
                sqlQuery = sqlQuery + "  Where MONTH(a.CreatedOn) = @month and c.DepartmentId =@dept group by a.UserId, b.FirstName,b.LastName,d.Name";
            }
            else if (month != null && dept != null && email != null)
            {
                sqlQuery = sqlQuery + " Where MONTH(a.CreatedOn) = @month and c.DepartmentId =@dept and b.NormalizedEmail =@email group by a.UserId, b.FirstName,b.LastName,d.Name";
            }

            List<EmployeeAttendanceModel> model = new List<EmployeeAttendanceModel>();

            var Get_Absent_Present = Query<EmployeeAttendanceModel>(sql: sqlQuery, param: new { month, dept, email,workingDays });
           
            foreach (var item in Get_Absent_Present)
            {
                model.Add(new EmployeeAttendanceModel { FirstName = item.FirstName, LastName = item.LastName, PresentDays = item.PresentDays, AbsentDays = workingDays - Convert.ToInt32(item.PresentDays) ,DepartmentName=item.DepartmentName});
            }

            return Get_Absent_Present;
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

        public IEnumerable<EmployeeAttendanceModel> EmployeeAttendance(string UserId, string month)
        {
            string query = @"select a.UserId,b.FirstName, b.LastName,a.SignIn,a.SignOut, a.CreatedOn  ,(SELECT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND,a.SignIn, a.SignOut),0), 108) as ElapsedTime) as TotalTime from [dbo].[Attendance] as a join [dbo].[AspNetUsers] b on a.UserId =b.Id  Where a.UserId= @UserId";

            if (month != null)
            {
                query = query + " and MONTH(a.CreatedOn) = @month";
            }

            var data = Query<EmployeeAttendanceModel>(sql: query, param: new { UserId, month });
            return data;
        }

        public ApplicationUser FindByNormalizedEmail(string normalizedEmail)
        {
            return QuerySingleOrDefault<ApplicationUser>(
                sql: "SELECT * FROM AspNetUsers WHERE NormalizedEmail = @normalizedEmail",
                param: new { normalizedEmail }
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



        public IEnumerable<EmployeeAttendanceModel> All()
        {
            var query = @"select b.FirstName, b.LastName,a.SignIn,a.SignOut, a.CreatedOn  ," +
               "(SELECT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND,a.SignIn, a.SignOut),0), 108) as ElapsedTime) as TotalTime," +
               "(SELECT CONVERT(VARCHAR(8), DATEADD(SECOND, DATEDIFF(SECOND,a.SignIn, (SELECT GETDATE())),0), 108) as ElapsedTime) as ResumeTime" +
               " from [dbo].[Attendance] as a join [dbo].[AspNetUsers] b on a.UserId =b.Id " +
               "join [dbo].[AspNetUserRoles] c on b.Id =c.UserId";
            var data = Query<EmployeeAttendanceModel>(sql: query);
            return data;
        }

        public IEnumerable<EmployeeAttendanceModel> GetAttendance(string userid, int workingDays)
        {
            var query = @"select count(a.UserId)as PresentDays,a.UserId,b.FirstName,b.LastName,(d.Name) as DepartmentName from [dbo].[Attendance] as a
                         join [dbo].[AspNetUsers] b on a.UserId =b.Id 
                           join [dbo].[AspNetUserRoles] c on b.Id =c.UserId
                           join [dbo].[Department] d on c.DepartmentId =d.Id group by a.UserId, b.FirstName,b.LastName,d.Name";
            if (userid != null)
            {
                query = query + " where a.UserId=@userid";
            }
            var data = Query<EmployeeAttendanceModel>(sql: query, param: new { userid });
            List<EmployeeAttendanceModel> model = new List<EmployeeAttendanceModel>();
            foreach (var item in data)
            {
                model.Add(new EmployeeAttendanceModel { FirstName = item.FirstName, LastName = item.LastName, PresentDays = item.PresentDays, AbsentDays = workingDays - Convert.ToInt32(item.PresentDays), DepartmentName=item.DepartmentName });
            }
            return model;
        }
    }
}
