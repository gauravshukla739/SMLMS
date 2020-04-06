using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class LeaveService : ILeaveService
    {
        private IUnitOfWork unitOfWork;
        private IUserService userService;

        public LeaveService(IUnitOfWork _unitOfWork, IUserService _userService)
        {
            unitOfWork = _unitOfWork;
            userService = _userService;
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
        public async Task<ServiceResponse> PostLeave(LeaveDto _leave)
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
        

        public async Task<ServiceResponse> DeleteLeave(string id)
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                    unitOfWork.LeaveRepositry.Remove(id);
                    unitOfWork.Commit();
                    response.IsSuccess = true;
                }

                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
        }
        public async Task<ServiceResponse> GetLeaveRequest(ClaimsPrincipal claim)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var loggedId = claim.Claims.First(x => x.Type == "UserId").Value;
                Guid id = Guid.Parse(loggedId);

                var data = unitOfWork.LeaveRepositry.GetLeaveRequest(id);
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
        public async Task<ServiceResponse> GetDataBasedOnId(ClaimsPrincipal claim)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var deptid = claim.Claims.Where(c => c.Type == "DepartmentId").Select(c => c.Value).SingleOrDefault();
                var RoleName = claim.Claims.First(x => x.Type == ClaimTypes.Role).Value;

                var data = unitOfWork.LeaveRepositry.GetDataBasedOnId(Guid.Parse(deptid), RoleName);
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


        public async Task<ServiceResponse> RequestLeave(RequestLeave _RequestLeave, ClaimsPrincipal claim)
        {
            {
                var loggedInUserEmailId = claim.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                var loggedInUserRoleId = claim.Claims.First(x => x.Type == "RoleId").Value;
                var loggedInUserUserId = claim.Claims.First(x => x.Type == "UserId").Value;
                var loggedInUserRoleName = claim.Claims.First(x => x.Type == ClaimTypes.Role).Value;
                var a = claim.Claims.Where(c => c.Type == "DepartmentId").Select(c => c.Value).SingleOrDefault();
                _RequestLeave.CreatedBy = loggedInUserEmailId;
                _RequestLeave.Userid =  Guid.Parse(loggedInUserUserId);

                ServiceResponse response = new ServiceResponse();
                try
                {
                    unitOfWork.LeaveRepositry.RequestLeave(_RequestLeave);
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


        public async Task<ServiceResponse> DeleteLeaveRequest(string id)
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                    unitOfWork.LeaveRepositry.RemoveRequest(id);
                    unitOfWork.Commit();
                    response.IsSuccess = true;
                }
                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
        }

        public async Task<ServiceResponse> ApproveLeaveRequest(Guid id, ClaimsPrincipal claim)
        {
            {
                ServiceResponse response = new ServiceResponse();
                try
                {
                    var aprovalByName = claim.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                    var loggedInUserUserId = claim.Claims.First(x => x.Type == "UserId").Value;
                    RequestLeave requestLeave = new RequestLeave
                    {
                        Id = id,
                        UpdatedBy = aprovalByName
                    };
                    unitOfWork.LeaveRepositry.ApproveLeaveRequest(requestLeave);
                    unitOfWork.Commit();
                    response.IsSuccess = true;
                }
                catch (Exception ex)
                {

                    response.IsSuccess = false;
                    response.Message = ex.ToString();
                }
                return response;
            }
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
                if (res != null)
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

























