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
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<ServiceResponse> Get()
        {
            return await _taskService.GetAllTasks();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ServiceResponse> Post(SMLMS.Model.Core.Task model)
        {
            //model.Id =  Guid.NewGuid();
            //model.CreateDate = DateTime.Now;
            return await _taskService.SaveUpdateTask(model, User);
        }        

        [HttpPost]
        [Route("Delete")]
        public async Task<ServiceResponse> Delete(Guid id)
        {
            return await _taskService.DeleteTask(id, User);
        }

        [HttpGet]
        [Route("GetTaskById/{Id}")]
        public async Task<ServiceResponse> GetTaskById(string id)
        {
            return await _taskService.GetTaskById(id);
        }

        [HttpGet]
        [Route("GetTaskByEmployeeId/{EmployeeId}")]
        public async Task<ServiceResponse> GetTaskByEmployeeId(Guid EmployeeId)
        {
            return await _taskService.GetTaskByEmployeeId(EmployeeId);
        }
        [HttpGet]
        [Route("GetMyTaskByEmployeeId/{EmployeeId}")]
        public async Task<ServiceResponse> GetMyTaskByEmployeeId(Guid EmployeeId)
        {
            return await _taskService.GetMyTaskByEmployeeId(EmployeeId);
        }
        
        [HttpGet]
        [Route("GetTaskByDepartmentId/{DepartmentId}")]
        public async Task<ServiceResponse> GetTaskByDepartmentId(Guid DepartmentId)
        {
            return await _taskService.GetTaskByDepartmentId(DepartmentId);
        }
    }
}
