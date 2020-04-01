using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.DTO
{
    public class DepartmentDto
    {
		    public Guid Id  { get; set; }
		    public string Name { get; set; }
		    public string RoleId { get; set; }
            public bool IsDeleted { get; set; }
            public DateTime? CreateDate { get; set; }
            public DateTime? UpdateDate { get; set; }
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
            public string IsDeletedBy { get; set; }


    }
}
