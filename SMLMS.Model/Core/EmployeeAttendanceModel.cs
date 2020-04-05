using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class EmployeeAttendanceModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? SignIn { get; set; }
        public DateTime? SignOut { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string TotalTime { get; set; }
    }
}
