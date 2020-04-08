using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;

namespace SMLMS.REST.Controllers
{

    public class EmployeeLeaveController : BaseController
    {
        private ILeaveService _leaveServicer;
        public EmployeeLeaveController(ILeaveService leaveService)
        {
            _leaveServicer = leaveService;
        }

        [HttpGet]
        public async Task<ServiceResponse> Get(Guid id, Guid deptId)
        {
            return await _leaveServicer.GetEmployeeLeaves( id,  deptId);
        }

        [HttpPost]
        public async Task<ServiceResponse> Get(EmpolyeeLeaveDto model)
        {
            return await _leaveServicer.SaveUpdateEmployeeLeave(model);
        }
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<ServiceResponse> Delete(Guid id)
        {
            return await _leaveServicer.DeleteEmployeeLeave(id);
        }



    }
}