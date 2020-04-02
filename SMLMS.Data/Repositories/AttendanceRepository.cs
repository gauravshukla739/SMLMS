using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;

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

        public void Add(Attendance entity)
        {
            var Exist = FindUserById(entity.EmployeeId);
            if (Exist.Id == null &&Exist != null)
            {
                Execute(
            sql: @"
                    INSERT INTO [dbo].[Attendance]([Id], [EmployeeId], [SignIn],
	                    [CreatedOn],  [CreatedBy],
	                   [IsDeleted])
                    VALUES(@Id, @EmployeeId, @SignIn, @CreatedOn,
	                    @CreatedBy, @IsDeleted)",
            param: entity);
            }
            else
            {
                var query = "UPDATE Attendance SET SignOut = @SignOut, UpdatedOn = @UpdatedOn, UpdatedBy = @UpdatedBy WHERE Id = @Id";
                Execute(
                sql: query,
                param: entity);
            }
        }



        public IEnumerable<Attendance> All()
        {
            return Query<Attendance>(
                sql: "SELECT * FROM [dbo].[Attendance]"
            );
        }

        public Attendance FindUserById(int key)
        {
            var query = "SELECT * FROM Attendance WHERE EmployeeId = @key and cast(CreatedOn as Date) = cast(getdate() as Date) order by CreatedOn desc";
            var data = QuerySingleOrDefault<Attendance>(sql: query, param: new { key });
            return data;
        }

        public User Find(string key)
        {
            return QuerySingleOrDefault<User>(
                sql: "SELECT * FROM Attendance WHERE Id = @key",
                param: new { key }
            );
        }

        public void Update(Attendance entity)
        {
            Execute(
                sql: @"
                    UPDATE Attendance SET SignOut = @SignOut,
	                    UpdatedOn = @UpdatedOn, UpdatedBy = @UpdatedBy
                    WHERE Id = @Id",
                param: entity);
        }


    }
}
