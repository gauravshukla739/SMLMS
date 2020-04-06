using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class LeaveService : ILeaveService
    {
        private IUnitOfWork unitOfWork;
        public LeaveService(IUnitOfWork _unitOfWork)
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



        public async Task<ServiceResponse> GetEmployeeLeaves(Guid id, Guid deptId)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
               response.Data  = unitOfWork.EmployeeLeaveRepository.GetEmployeeLeaves(id, deptId);
               // var aa1 = unitOfWork.EmployeeLeaveRepository.GetEmployeeLeaves(id, deptId);
               //  var aa = (List<EmpolyeeLeaveDto>)aa1;
               // var aa12 = aa.GroupBy(x => new { x.UserId, x.EmpName}).Select(x => new
               // {
               //     id = x.Key.UserId,
               //     name = x.Key.EmpName,
               //     list = x.ToList()

               // }).ToList();
                response.IsSuccess = true;
                response.Message = "Success";

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }
        public async Task<ServiceResponse> SaveUpdateEmployeeLeave(EmpolyeeLeaveDto model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var rec = new EmpolyeeLeave
                {
                    Id = model.Id,
                    LeaveCount = model.LeaveCount,
                    LeaveTypeId = model.LeaveTypeId,
                    UserId = model.UserId
                };
                var res = unitOfWork.EmployeeLeaveRepository.Find(Convert.ToString(model.Id));
                if(res != null)
                {
                    model.UpdateDate = DateTime.Now;
                    model.UpdatedBy = "";
                    unitOfWork.EmployeeLeaveRepository.Update(rec);
                }
                else
                {
                    model.CreateDate = DateTime.Now;
                    model.CreatedBy = "";
                    unitOfWork.EmployeeLeaveRepository.Add(rec);
                }
                unitOfWork.Commit();
                response.IsSuccess = true;
                response.Message = "Success";

            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }
        public async Task<ServiceResponse> DeleteEmployeeLeave(Guid id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                unitOfWork.EmployeeLeaveRepository.Remove(Convert.ToString(id));
                unitOfWork.Commit();
                response.IsSuccess = true;
                response.Message = "Success";

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

























