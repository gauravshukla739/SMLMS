using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class EmpolyeeLeave : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int LeaveCount { get; set; }


    }
}
