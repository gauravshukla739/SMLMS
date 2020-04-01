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
        Task<ServiceResponse> GetAll();
        Task<ServiceResponse> Add(Attendence attendence);
    }
}
