
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IRoleRepository : IRepository<Role, string>
    {
        Role FindByName(string roleName);
    }
}
