using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
   public class RequestLeave
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DayBegin { get; set; }
        public string DayEnd { get; set; }
        public string ShortLeaveFrom { get; set; }
        public String ShortLeaveTo { get; set; }
        public String Reason { get; set; }

    }
}
