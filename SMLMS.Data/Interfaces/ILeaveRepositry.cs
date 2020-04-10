using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SMLMS.Data.Interfaces
{
    public interface ILeaveRepositry : IRepository<LeaveDto, string>
    {

        void UpdateData(LeaveDto model,string id);
        void RequestLeave(RequestLeave model); 
        IEnumerable<RequestLeave> GetLeaveRequest(Guid id);
        IEnumerable<RequestLeave> GetDataBasedOnId(Guid deptid, string RoleName);

        void ApproveLeaveRequest( RequestLeave requestLeave);

        void RejectLeaveRequest(RequestLeave requestLeave);
        void RemoveRequest(string key);

        IEnumerable<RequestLeave> FindByDepartmentId(Guid departmentId);
    }
}
