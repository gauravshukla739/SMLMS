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
        [Route("Put")]
        public async Task<ServiceResponse> Put(LeaveDto model,string id)
        {
            return await _leave.UpdateLeave(model,id);
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<ServiceResponse> Delete(string id)
        {
            return await _leave.DeleteLeave(id);
        }

        [HttpPost]
        [Route("RequestLeave")]
        public async Task<ServiceResponse> RequestLeave(RequestLeave model,string id)
        {
            return await _leave.RequestLeave(model, id);
        }

        [HttpGet]
        [Route("GetLeaveRequest")]
        public async Task<ServiceResponse> GetLeaveRequest()
          {
            return await _leave.GetLeaveRequest();
        }
    }
}
