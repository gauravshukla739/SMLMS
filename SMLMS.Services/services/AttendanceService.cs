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
    public class AttendanceService : IAttendanceService
    {
        private IUnitOfWork unitOfWork;
        private IAuthenticationService _authenticationService;
        public AttendanceService(IUnitOfWork _unitOfWork, IAuthenticationService authenticationService)
        {
            unitOfWork = _unitOfWork;
            authenticationService = _authenticationService;
        }

        public async Task<ServiceResponse> CreateOrUpdate(ClaimsPrincipal claims)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var userId= claims.Claims.First(x => x.Type == "UserId").Value;
                var Exist = unitOfWork.AttendanceRepository.FindUserById(userId);

                AttendanceDto data = new AttendanceDto
                {
                    Id = Guid.NewGuid().ToString(),
                    SignIn = DateTime.Now,
                    SignOut = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    UserId = userId,
                    IsDeleted = false,
                };


                if (Exist == null)
                {
                    unitOfWork.AttendanceRepository.Add(data);
                }
                else
                {
                    data.Id = Exist.Id.ToString();
                    unitOfWork.AttendanceRepository.Update(data);
                }
                unitOfWork.Commit();
                response.Data = data;
                response.IsSuccess = true;
                response.Message = "Data Saved Successfully!";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

       

        public Task<ServiceResponse> Delete(string Name)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Fetch";
                response.Data = unitOfWork.AttendanceRepository.All();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> Employess(AttendanceDto model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Fetch";
                response.Data = unitOfWork.AttendanceRepository.EmployeeAttendance_DateFilter(model.startDate, model.endDate);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> GetEmployeAttendance(string userid)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Fetch";
                response.Data = unitOfWork.AttendanceRepository.EmployeeAttendance(userid);
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
