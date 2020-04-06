using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace SMLMS.Data.Interfaces
{
    public interface IEmployeeLeaveRepository : IRepository<EmpolyeeLeave, string>
    {
        object GetEmployeeLeaves(Guid id, Guid deptId);
    }
}
