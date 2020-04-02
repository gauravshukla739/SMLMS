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
        public AttendanceService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<ServiceResponse> CreateOrUpdate(Attendance model)
        {
            model.SignIn = DateTime.Now;
            model.SignOut = DateTime.Now;
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;
            model.UpdatedBy = 1;
            model.CreatedBy = 1;    //Login employeeId
            model.EmployeeId = 1;    //Login employeeId
            model.IsDeleted = false;

            ServiceResponse response = new ServiceResponse();
            try
            {
                unitOfWork.AttendanceRepository.Add(model);
                unitOfWork.Commit();
                response.IsSuccess = true;
                response.Message = "Punch in successfully..!!";
                response.Data = model;

                // var s = unitOfWork.AttendanceRepository.All();
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
                response.Data = model;
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
