using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
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
                var userId = claims.Claims.First(x => x.Type == "UserId").Value;
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
                response.Data = unitOfWork.AttendanceRepository.All(null, null, null,0);
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

        public async Task<ServiceResponse> GetEmployeAttendance(string userid, string role, string month, string dept, string user)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Fetch";
                if (role == "Admin")
                {

                    var workingDays = GetWorkingDays();

                    response.Data = unitOfWork.AttendanceRepository.All(month, dept, user, workingDays);
                }
                else
                {
                    response.Data = unitOfWork.AttendanceRepository.EmployeeAttendance(userid, month);
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> FindByEmail(string email)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                response.Message = "Data Fetch";
                response.Data = unitOfWork.AttendanceRepository.FindByNormalizedEmail(email);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> GetPresent_AbsentDays_Emp(string userid, string role)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var workingDays = GetWorkingDays();

                response.IsSuccess = true;
                response.Message = "Data Fetch";
                response.Data = null;
              
               
                if (role == "Admin")
                {
                    response.Data = unitOfWork.AttendanceRepository.GetAttendance(null, workingDays);
                   

                }
                else
                {
                    response.Data = unitOfWork.AttendanceRepository.GetAttendance(userid, workingDays);
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Task<ServiceResponse> GetPresent_AbsentDays_Emp(string userid)
        {
            throw new NotImplementedException();
        }


        public int GetWorkingDays()
        {
            var lastDayOfMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            int totalWeekend_Days = 0;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);
                switch (testDate.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        Console.WriteLine(testDate.ToShortDateString());
                        totalWeekend_Days++;
                        break;
                }
            }

            int workingDays = DateTime.Now.Day - totalWeekend_Days;
            return workingDays;
        }
    }
}
