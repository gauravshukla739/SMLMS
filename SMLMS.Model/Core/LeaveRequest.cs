using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class LeaveRequest : BaseEntity
    {
        public int Id { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DayBegin { get; set; }
        public string DayEnd { get; set; }
        public int LeaveTypeId { get; set; }
        public int ShortLeaveFrom { get; set; }
        public int ShortLeaveTo { get; set; }
        public string Reason { get; set; }
        public string RoleId { get; set; }

    }
}
