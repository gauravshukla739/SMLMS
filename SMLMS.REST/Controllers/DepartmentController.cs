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
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<ServiceResponse> CreateOrUpdate(DepartmentDto model)
        {
            return await _departmentService.CreateOrUpdate(model);
        }
    }
}