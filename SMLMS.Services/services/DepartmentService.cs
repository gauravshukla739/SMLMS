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
            try
            {
                if (model.Id != null)
                {
                    _unitOfWork.DepartmentRepository.Add(model);
                    _unitOfWork.Commit();
                }
                else
                {
                    _unitOfWork.DepartmentRepository.Update(model);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> Delete(Guid Id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                _unitOfWork.DepartmentRepository.Remove(Id,"");
                _unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> All()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {              
                response.IsSuccess = true;
                response.Message = "Data Fetch";
                response.Data = _unitOfWork.DepartmentRepository.All();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> GetById(Guid Id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Fetch By Id";
                response.Data = _unitOfWork.DepartmentRepository.FindById(Id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

       
    }
}
