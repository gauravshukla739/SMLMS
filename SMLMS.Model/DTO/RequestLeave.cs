using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
   public class RequestLeave : BaseEntity
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DayBegin { get; set; }
        public string DayEnd { get; set; }
        public string ShortLeaveFrom { get; set; }
        public String ShortLeaveTo { get; set; }
        public String Reason { get; set; }
        public Guid Userid { get; set; }
        public dynamic  isApproved { get; set; }
        public dynamic isRejected { get; set; }
        public string RejectionReason { get; set; }
        public string ApproveStatus { get; set; }
        public Guid DepartmentId { get; set; }





    }
}
