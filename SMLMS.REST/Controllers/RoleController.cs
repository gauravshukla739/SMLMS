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
   
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ServiceResponse> All()
        {
            return await _roleService.GetAllRoles();
        }

        [HttpPost]
        public async Task<ServiceResponse> Post(RoleDto roleDto)
        {
            return await _roleService.SaveUpdateRole(roleDto, User);
        }

        [HttpPost]
        [Route("Permission")]
        public async Task<ServiceResponse> Permission(List<RoleTaskPermissionDto> roleDto)
        {
            return await _roleService.AddRoleToTask(roleDto, User);
        }
        [HttpGet]
        [Route("Permission")]
        public async Task<ServiceResponse> GetPermission()
        {
            return await _roleService.GetAllPermission();
        }

       
    }
}