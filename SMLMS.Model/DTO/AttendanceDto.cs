using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
   public class AttendanceDto
    {
        public string EmployeeId { get; set; }
        public string SignIn { get; set; }
        public string SignOut { get; set; }
        public string CreatedOn { get; set; }
        public string UpdateOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
