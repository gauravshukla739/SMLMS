using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
