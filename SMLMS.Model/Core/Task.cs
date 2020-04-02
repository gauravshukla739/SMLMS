using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid DepartmentId { get; set; }
        public string EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string AdminComment { get; set; }
        public string EmployeeName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
