using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface IDepartmentService
    {
        Task<ServiceResponse> CreateOrUpdate(DepartmentDto Name);
        Task<ServiceResponse> Delete(string Name);
        Task<ServiceResponse> Get();

    }
}
