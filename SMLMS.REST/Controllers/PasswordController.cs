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
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;
        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<ServiceResponse> Forgot([FromBody] string EmailId)
        {
            return await  _passwordService.Forgot(EmailId);
        }

        [HttpPost]
        public async Task<ServiceResponse> Reset(ResetPasswordDto model)
        {
            return await _passwordService.Reset(model);
        }

        [HttpPost]
        public async Task<ServiceResponse> Change(ChangePasswordDto model)
        {
            return await _passwordService.Change(model); 
        }
    }
}