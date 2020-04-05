using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
   public class LeaveDto 
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
