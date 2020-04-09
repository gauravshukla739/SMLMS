using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;

namespace SMLMS.REST.Controllers
{
    public class LeaveController : BaseController
    {
        private ILeaveService _leave;
        public LeaveController(ILeaveService leave)
        {
            _leave = leave;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ServiceResponse> Get()
        {
            return await _leave.GetLeaveType();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ServiceResponse> Save_Update(LeaveDto model)
        {
            return await _leave.PostLeave(model);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ServiceResponse> Delete(string id)
        {
            return await _leave.DeleteLeave(id);
        }

        [HttpPost]
        [Route("RequestLeave")]
        public async Task<ServiceResponse> RequestLeave(RequestLeave model)
        {
            return await _leave.RequestLeave(model,User);
        }

        [HttpGet]
        [Route("GetLeaveRequest")]
        public async Task<ServiceResponse> GetLeaveRequest()
          {
            return await _leave.GetLeaveRequest(User);
        }
        [HttpPost]
        [Route("DeleteLeaveRequest")]
        public async Task<ServiceResponse> DeleteLeaveRequest(string id)
        {
            return await _leave.DeleteLeaveRequest(id);
        }

        [HttpGet]
        [Route("GetDataBasedOnId")]
        public async Task<ServiceResponse> GetDataBasedOnId()
        {
            return await _leave.GetDataBasedOnId(User);
        }
        [HttpPost]
        [Route("ApproveLeaveRequest")]
        public async Task<ServiceResponse> ApproveLeaveRequest(Guid id)
        {
            return await _leave.ApproveLeaveRequest(id,User);
        }

        [HttpPost]
        [Route("RejectLeaveRequest")]
        public async Task<ServiceResponse> RejectLeaveRequest(RejectLeave model)
        {
            return await _leave.RejectLeaveRequest(model, User);
        }

        [HttpGet]
        [Route("getLeaveByDepartmentId")]
        public async Task<ServiceResponse> GetLeaveByDepartmentId(Guid DepartmentId)
        {
            return await _leave.GetLeaveByDepartmentId(DepartmentId);
        }

    }
}
