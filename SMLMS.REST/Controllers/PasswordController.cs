﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.DTO;
using SMLMS.Services.interfaces;

namespace SMLMS.REST.Controllers
{
   
    public class PasswordController : BaseController
    {
        private readonly IPasswordService _passwordService;
        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet]
        [Route("Forgot")]
        [AllowAnonymous]
        public async Task<ServiceResponse> Forgot(string emailId)
        {
            return await  _passwordService.Forgot(emailId);
        }

        [HttpPost]
        [Route("Reset")]
        [AllowAnonymous]
        public async Task<ServiceResponse> Reset(ResetPasswordDto model)
        {
            return await _passwordService.Reset(model);
        }

        [HttpPost]
        [Route("Change")]
        public async Task<ServiceResponse> Change(ChangePasswordDto model)
        {
            return await _passwordService.Change(model,User); 
        }
    }
}