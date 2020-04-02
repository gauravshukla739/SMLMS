using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace SMLMS.Data.Interfaces
{
    public interface ILeaveRepositry : IRepository<LeaveDto, string>
    {

        void UpdateData(LeaveDto model,string id);
        void RequestLeave(RequestLeave model); 

        void UpdateRequestLeave(RequestLeave model,string id);

        IEnumerable<RequestLeave> GetLeaveRequest();
        /*Leave FindByName(string roleName);*/ // this is extra method apart from already defined methods which are all comman or we can say is fulfilling our needs.

        // No custom need of any method yet , requirement is getting fulfilled by all defined methods in 'IRepository' which is inheriting in 'ILeaveRepositry'    
    }
}
