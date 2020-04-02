using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IDepartmentRepository : IRepository<DepartmentDto, string>
    {
        DepartmentDto FindById(Guid key);
        void  Remove(Guid key, string userName);
    }
}
