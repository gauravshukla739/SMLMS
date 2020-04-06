using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IAttendanceRepository
    {
        void Add(AttendanceDto attendence);
        void Update(AttendanceDto attendence);
        IEnumerable<EmployeeAttendanceModel> All();
        IEnumerable<Attendance> EmployeeAttendance_DateFilter(string startDate, string endDate);
        Attendance FindUserById(string UserId);
    }
}
