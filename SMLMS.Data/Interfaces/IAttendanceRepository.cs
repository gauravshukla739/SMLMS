using SMLMS.Model.Core;

using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendence);
        void Update(Attendance attendence);
        IEnumerable<Attendance> All();
        IEnumerable<Attendance> EmployeeAttendance_DateFilter(DateTime? startDate, DateTime? endDate);
        Attendance FindUserById(int EmployeeId);
    }
}
