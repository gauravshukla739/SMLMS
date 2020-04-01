using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IDepartmentRepository 
    {
        void Add(DepartmentDto entity);
        IEnumerable<DepartmentDto> All();
    }
}
