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
        public string ResumeTime { get; set; }
        public  Guid UserId { get; set; }
        public string PresentDays { get; set; }
        public int? AbsentDays { get; set; }
        public string WorkingDays { get; set; }
        public string CurrentMonth { get; set; }
        public string DepartmentName { get; set; }

    }
}
