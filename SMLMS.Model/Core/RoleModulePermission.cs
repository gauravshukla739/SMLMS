using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class RoleModulePermission : BaseEntity
    {
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string ModuleId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
    }
}
