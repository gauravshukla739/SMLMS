using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
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
        public async Task<ServiceResponse> CreateOrUpdate(Attendance model)
        {
            return await _attendanceService.CreateOrUpdate(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ServiceResponse> Create(Attendance model)
        {
            return await _attendanceService.CreateOrUpdate(model);
        }

    }
}