using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public  class Module : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

    }
}
