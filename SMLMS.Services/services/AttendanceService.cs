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
        public async Task<ServiceResponse> Add(Attendence attendence)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = unitOfWork.AttendanceRepository.AddUserAttendance(attendence);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public Task<ServiceResponse> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
