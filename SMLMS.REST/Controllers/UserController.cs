using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Services.interfaces;

namespace SMLMS.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ServiceResponse> GetAll()
        {
            return await _userService.GetAll();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ServiceResponse> DeleteUser(string userId)
        {
            return await _userService.Delete(userId);
        }
    }
}