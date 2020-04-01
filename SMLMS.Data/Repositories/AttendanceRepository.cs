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

        public void Add(Attendence  attendence )
        {
            Execute(
                sql: @"
                    INSERT INTO Attendance(Id, EmployeeId, SignIn, SignOut,
	                    CreatedOn, UpdatedOn, CreatedBy, UpdatedBy,
	                    IsDeleted)
                    VALUES(@Id, @EmployeeId, @SignIn, @SignOut, @CreatedOn,
	                    @UpdatedOn, @CreatedBy, @UpdatedBy, @IsDeleted)",
                param: attendence
            );
        }

        public IEnumerable<Attendence> All()
        {
            throw new NotImplementedException();
        }

        public Attendence Find(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(Attendence entity)
        {
            throw new NotImplementedException();
        }
    }
}
