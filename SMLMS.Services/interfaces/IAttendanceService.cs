using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IAttendanceService
    {
        Task<ServiceResponse> CreateOrUpdate(Attendance attendance);
        Task<ServiceResponse> Delete(string Name);
        Task<ServiceResponse> Get();

    }
}
