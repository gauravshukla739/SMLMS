using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;

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

        public async Task<ServiceResponse> CreateOrUpdate(Attendance model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.IsSuccess = true;
                if (model.Id != null)
                {
                    model.SignIn = DateTime.Now;
                    model.CreatedOn = DateTime.Now;
                    model.CreatedBy = "1";  //(string)_authenticationService.GetClaimsValue("Email");
                    //model.EmployeeId = "1";
                    unitOfWork.AttendanceRepository.Add(model);
                    unitOfWork.Commit();
                    response.Message = "Data Saved Successfully!";

                }
                else
                {
                    model.SignOut = DateTime.Now;
                    model.UpdatedOn = DateTime.Now;
                    model.UpdatedBy = "1";//(string)_authenticationService.GetClaimsValue("Email");
                    unitOfWork.AttendanceRepository.Update(model);
                    unitOfWork.Commit();
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
