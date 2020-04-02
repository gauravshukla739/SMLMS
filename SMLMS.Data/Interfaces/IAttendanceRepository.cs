using SMLMS.Model.Core;

using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendence);
        IEnumerable<Attendance> All();

        
        Attendance FindUserById(int EmployeeId);
    }
}
