
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IModuleRepository : IRepository<Module, string>
    {
        Module FindByName(string moduleName);
    }
}
