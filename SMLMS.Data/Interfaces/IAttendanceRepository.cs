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
        IEnumerable<EmployeeAttendanceModel> All(string month, string dept, string user,int workingDays);
        IEnumerable<Attendance> EmployeeAttendance_DateFilter(string startDate, string endDate);
        Attendance FindUserById(string UserId);
        IEnumerable<EmployeeAttendanceModel> EmployeeAttendance(string UserId, string month);
        ApplicationUser FindByNormalizedEmail(string normalizedEmail);
        IEnumerable<EmployeeAttendanceModel> GetAttendance(string userid, int workingDays);
        IEnumerable<EmployeeAttendanceModel> getAllEmployess();
        IEnumerable<EmployeeAttendanceModel> GetTodayPunchIn();
        IEnumerable<EmployeeAttendanceModel> GetEmployeeAttendance(string userid, int workingDays);
    }
}
