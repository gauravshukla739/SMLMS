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
    public class AttendanceService : IAttendanceService
    {
        private IUnitOfWork unitOfWork;
        private IAuthenticationService _authenticationService;
        public AttendanceService(IUnitOfWork _unitOfWork,IAuthenticationService authenticationService)
        {
            unitOfWork = _unitOfWork;
            authenticationService = _authenticationService;
        }

        public async Task<ServiceResponse> CreateOrUpdate(AttendanceDto model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var Exist = unitOfWork.AttendanceRepository.FindUserById("7D4E4733-5E63-427D-3AF2-08D7D736AD59");

                AttendanceDto data = new AttendanceDto
                {
                    Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid().ToString() : model.Id,
                    SignIn = DateTime.Now,
                    SignOut = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    UpdateOn = DateTime.Now,
                    UserId = "7D4E4733-5E63-427D-3AF2-08D7D736AD59",
                    IsDeleted = false,
                };


                if (string.IsNullOrEmpty(model.Id) && Exist.SignIn == null)
                    unitOfWork.AttendanceRepository.Add(data);
                else
                    data.Id = Exist.Id.ToString();
                    unitOfWork.AttendanceRepository.Update(data);
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

        public Task<ServiceResponse> Get()
        {
            throw new NotImplementedException();
        }
    }
}
