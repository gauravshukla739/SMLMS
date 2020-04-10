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
        public async Task<ServiceResponse> EmployeeAttendance(string userid, string role)
        {
            if (role == "Admin")
            {
                return await _attendanceService.GetAll();
            }
            else
            {
                return await _attendanceService.GetEmployeAttendance(userid, role, null, null, null);
            }

        }

        [HttpGet]
        [Route("AttendanceFilter")]
        public async Task<ServiceResponse> AttendanceFilter(string userid,string role, string month, string dept, string user)
        {
            return await _attendanceService.GetEmployeAttendance(userid, role == "undefined" ? null : role, month == "undefined" ? null : month, dept == "undefined" ? null : dept, user == "undefined" ? null : user);
        }

        [HttpGet]
        [Route("FindByEmail")]
        public async Task<ServiceResponse> FindByEmail(string email)
        {
            return await _attendanceService.FindByEmail(email);
        }

        [HttpGet]
        [Route("Employees_Absent_Present")]
        public async Task<ServiceResponse> Employees_Absent_Present(string userid, string role)
        {
            return await _attendanceService.GetPresent_AbsentDays_Emp(userid, role == "undefined" ? null : role);
        }

        [HttpGet]
        [Route("getAllEmployess")]
        public async Task<ServiceResponse> getAllEmployess(string userid, string role ,string dept)
        {
            if(role == "Team Lead")
            {
                return await _attendanceService.GetEmployeAttendance(userid, role == "undefined" ? null : role, null, dept == "undefined" ? null : dept, null);
            }
            else
            {
                return await _attendanceService.getAllEmployess();
            }
           
        }

        [HttpGet]
        [Route("getTodayPunchIn")]
        public async Task<ServiceResponse> getTodayPunchIn()
        {
            return await _attendanceService.GetTodayPunchIn();
        }



        [HttpGet]
        [Route("GetEmployeeatendanceDetails")]
        public async Task<ServiceResponse> GetEmployeeatendanceDetails(string userid)
        {
            return await _attendanceService.GetEmployeeatendanceDetails(userid);
        }
        
    }
}