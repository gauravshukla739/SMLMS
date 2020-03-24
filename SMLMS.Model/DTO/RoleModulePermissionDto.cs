using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class RoleModulePermissionDto
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string ModuleId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
