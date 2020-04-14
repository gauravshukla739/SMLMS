using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Services.interfaces;

namespace SMLMS.REST.Controllers
{
   
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]     
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ServiceResponse> Login(UserDto user)
        {
            return await _authenticationService.SignIn(user,User);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ServiceResponse> Register(UserDto user)
        {
            return await  _authenticationService.CreateUser(user,User,"register");
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ServiceResponse> Update(UserDto user)
        {
            return await _authenticationService.Update(user,User);
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<ServiceResponse> Detail(string id)
        {
            return await _authenticationService.Detail(id);
        }

        [HttpGet]
        [Route("Logout")]
        public  Task<ServiceResponse> Logout()
        {
            return  _authenticationService.Logout();
        }
    }
}