using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
   public class Attendence
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? SignIn { get; set; }
        public DateTime? SignOut { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}
