using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ServiceResponse> CreateOrUpdate()
        {
            return await _attendanceService.CreateOrUpdate(User);
        }

        [HttpPost]
        [Route("EmployeeAttendancess")]
        public async Task<ServiceResponse> EmployeeAttendanceee(AttendanceDto model)
        {
            return await _attendanceService.Employess(model);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _attendanceService.GetAll();
        }

        [HttpGet]
        [Route("EmployeeAttendance")]
        public async Task<ServiceResponse> EmployeeAttendance()
        {
            return await _attendanceService.GetAll();
        }

    }
}