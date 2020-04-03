using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;

namespace SMLMS.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        //[HttpGet]
        //[Route("CreateOrUpdate")]
        //public async Task<ServiceResponse> CreateOrUpdate(Attendence model)
        //{
        //    return await _attendanceService.CreateOrUpdate(model);
        //}


        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<ServiceResponse> CreateOrUpdate(AttendanceDto model)
        {
            return await _attendanceService.CreateOrUpdate(model);
        }

        [HttpPost]
        [Route("EmployeeAttendance")]
        public async Task<ServiceResponse> EmployeeAttendance(AttendanceDto model)
        {
            return await _attendanceService.GetEmployess(model);
        }

    }
}