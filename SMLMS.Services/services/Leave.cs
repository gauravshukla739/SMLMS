using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class Leave : ILeave
    {
        private IUnitOfWork unitOfWork;
        public Leave(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<ServiceResponse> GetLeaveType()
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var data = unitOfWork.LeaveRepositry.All();
                unitOfWork.Commit();
                {
                    _response.IsSuccess = true;
                    _response.Message = "Sucess";
                    _response.Data = data;
                }
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }
        public async Task<ServiceResponse> PostLeave(LeaveDto _leave )
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                        unitOfWork.LeaveRepositry.Add(_leave);
                        unitOfWork.Commit();
                        response.IsSuccess = true;
                        response.Message = "Data Added";
                   
                }
                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
        }
        public async Task<ServiceResponse> UpdateLeave(LeaveDto _leave, string id)
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                        unitOfWork.LeaveRepositry.UpdateData(_leave,id);
                        unitOfWork.Commit();
                        response.IsSuccess = true;
                        response.Message = "Data Added";
                   
                 }
                
                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
        }
        public async Task<ServiceResponse> DeleteLeave(string id)
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                    unitOfWork.LeaveRepositry.Remove(id);
                    unitOfWork.Commit();
                    response.IsSuccess = true;
                    response.Message = "Data Added";

                }

                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
        }
        public async Task<ServiceResponse> GetLeaveRequest()
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var data = unitOfWork.LeaveRepositry.GetLeaveRequest();
                unitOfWork.Commit();
                {
                    _response.IsSuccess = true;
                    _response.Message = "Sucess";
                    _response.Data = data;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }
        public async Task<ServiceResponse> RequestLeave(RequestLeave _RequestLeave,string id)
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                    if ( string.IsNullOrEmpty(_RequestLeave.Id) &&  string.IsNullOrEmpty(id))
                    {
                        unitOfWork.LeaveRepositry.RequestLeave(_RequestLeave); // create
                        
                        unitOfWork.Commit();
                        response.IsSuccess = true;
                        response.Message = "Success";
                    }
                    else if (string.IsNullOrEmpty(_RequestLeave.Id) && id.Length > 0)
                    {
                        unitOfWork.LeaveRepositry.UpdateRequestLeave(_RequestLeave, id); // update
                        unitOfWork.Commit();
                        response.IsSuccess = true;
                        response.Message = "Success";
                    }

                }
                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
        }






    }
}

























