using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
   public class AttendanceDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime? SignIn { get; set; }
        public DateTime? SignOut { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
