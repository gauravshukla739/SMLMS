using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Model.Core
{
    public class Role
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
