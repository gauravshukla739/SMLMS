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
        public async Task<ServiceResponse> CreateOrUpdate(DepartmentDto model)
        {
            return await _departmentService.CreateOrUpdate(model);
        }

        [HttpGet("{Id}")]
        public async Task<ServiceResponse> GetById(Guid Id)
        {
            return await _departmentService.GetById(Id);
        }
        [HttpGet]
        public async Task<ServiceResponse> All()
        {
            return await _departmentService.All();
        }

        [HttpDelete("{Id}")]
        public async Task<ServiceResponse> Delete(Guid Id)
        {
            return await _departmentService.Delete(Id);
        }
    }
}