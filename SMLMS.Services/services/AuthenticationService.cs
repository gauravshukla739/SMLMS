﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
     public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
  
        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

      
        public async Task<ServiceResponse> SignIn(UserDto user)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var result =  await _signInManager.PasswordSignInAsync(user.UserName , user.Password, user.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    response.IsSuccess = true;
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == user.UserName);
                    var userDetail = await _userManager.FindByEmailAsync(user.Email);
                    var role = await _userManager.GetRolesAsync(userDetail);
                    var getRole =await _roleManager.FindByNameAsync(role[0]);                   
                    response.Data= new {  token = await GenerateJwtToken(user.UserName, appUser,role[0], getRole.Id.ToString()) };
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        response.IsSuccess = false;
                        response.Message = "Your account is locked";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Invalid user";
                    }
                }
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> CreateUser(UserDto user)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var appUser = new ApplicationUser { UserName = user.Email, Email = user.Email,Address=user.Address, CreatedBy=user.Email,DateOfAppointment=user.DateOfAppointment,DateOfBirth=user.DateOfBirth,DateOfJoin=user.DateOfJoin,DateOfLeave=user.DateOfLeave,FirstName=user.FirstName,LastName=user.LastName,PhoneNumber=user.PhoneNumber};
                var result = await _userManager.CreateAsync(appUser, user.Password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, user.RoleName);
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid user";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }


        public async Task<ServiceResponse> Update(UserDto user)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var email = (string)GetClaimsValue("Email");

                var userDetail = await _userManager.FindByNameAsync(email);
                userDetail.FirstName= user.FirstName;
                userDetail.LastName = user.LastName;
                userDetail.Address = user.Address;
                userDetail.DateOfAppointment = user.DateOfAppointment;
                userDetail.DateOfBirth = user.DateOfBirth;
                userDetail.DateOfJoin = user.DateOfJoin;
                userDetail.DateOfLeave = user.DateOfLeave;
                userDetail.PhoneNumber = user.PhoneNumber;
                userDetail.UpdateDate = DateTime.Now;
                userDetail.UpdatedBy = email;
                await _userManager.UpdateAsync(userDetail);
                response.IsSuccess = true;
                response.Message = "Data Updated Successfully";
               
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        private async Task<object> GenerateJwtToken(string email, ApplicationUser user,string role,string roleId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.FirstName+" "+user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,role),
                new Claim("RoleId",roleId),
                new Claim("DepartmentId",user.DepartmentId ),
                 new Claim("UserId",user.Id.ToString() )
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Set current principal
            Thread.CurrentPrincipal = claimsPrincipal;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return  new  JwtSecurityTokenHandler().WriteToken(token);
        }

        public object GetClaimsValue(string type)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var obj= new object();
            switch (type)
            {
                case "RoleId":
                    obj = identity.Claims.Where(c => c.Type == "RoleId").Select(c => c.Value).SingleOrDefault();
                    break;
                case "Role":
                    obj = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                    break;
                case "Email":
                    obj = identity.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
                    break;
                case "Name":
                    obj = identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                    break;
                case "DepartmentId":
                    obj = identity.Claims.Where(c => c.Type == "DepartmentId").Select(c => c.Value).SingleOrDefault();
                    break;
                case "UserId":
                    obj = identity.Claims.Where(c => c.Type == "UserId").Select(c => c.Value).SingleOrDefault();
                    break;
            }
            return obj;
        }

        public async Task<ServiceResponse> CreateRole(string roleName)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new Role { Name=roleName });
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Role already exist";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> Logout()
        {
            var response = new ServiceResponse();
            try
            {
                await _signInManager.SignOutAsync();
                response.IsSuccess = true;
                response.Message = "Logout Successfully!";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
