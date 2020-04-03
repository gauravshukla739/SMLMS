using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class DepartmentService : IDepartmentService
    {
        private IUnitOfWork _unitOfWork;
        private IAuthenticationService _authenticationService;
        public DepartmentService(IUnitOfWork unitOfWork,IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }
        public async Task<ServiceResponse> CreateOrUpdate(DepartmentDto model, ClaimsPrincipal claims)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                var emailId = claims.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                if (model.Id != null)
                {
                    model.CreatedBy = emailId;
                    _unitOfWork.DepartmentRepository.Add(model);
                    _unitOfWork.Commit();
                    response.Message = "Data Saved Successfully!";

                }
                else
                {
                    model.UpdateDate = DateTime.Now;
                    model.UpdatedBy= emailId;
                    _unitOfWork.DepartmentRepository.Update(model);
                    _unitOfWork.Commit();
                    response.Message = "Data Updated Successfully!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> Delete(Guid Id, ClaimsPrincipal claims)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                string deletedBy= claims.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                _unitOfWork.DepartmentRepository.Remove(Id,deletedBy);
                _unitOfWork.Commit();
                response.IsSuccess = true;
                response.Message = "Data Deleted ";

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
