using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class DepartmentService : IDepartmentService
    {
        private IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResponse> CreateOrUpdate(DepartmentDto model)
        {
            ServiceResponse response = new ServiceResponse();
            _unitOfWork.DepartmentRepository.Add(model);
            _unitOfWork.Commit();
          var s=  _unitOfWork.DepartmentRepository.All();
            return response;
        }

         public async Task<ServiceResponse> Delete(string Name)
        {
            ServiceResponse response = new ServiceResponse();

            return response;
        }

        public async Task<ServiceResponse> Get()
        {
            ServiceResponse response = new ServiceResponse();

            return response;
        }
    }
}
