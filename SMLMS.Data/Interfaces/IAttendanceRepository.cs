using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendence, string>
    {
        Attendence AddUserAttendance(Attendence attendence);
    }
}
