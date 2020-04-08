using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
    public class EmpolyeeLeaveDto
    {
        public Guid Id { get; set; }

        public string EmpName { get; set; }

        public string LeaveType { get; set; }
        public Guid UserId { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int LeaveCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string IsDeletedBy { get; set; }
    }
}
