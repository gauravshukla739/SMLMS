using Microsoft.AspNetCore.Identity;
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
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == user.Email);
                    response.Data= new {  token = await GenerateJwtToken(user.Email, appUser) };
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

        public async Task<ServiceResponse> CreateUser(UserDto _user)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = new ApplicationUser { UserName = _user.Email, Email = _user.Email};
                var result = await _userManager.CreateAsync(user, _user.Password);
                   
                //Role user1 = new Role() {Id=Guid.NewGuid(), Name="Abc" ,ConcurrencyStamp="234",NormalizedName="Abc"};
                //_unitOfWork.RoleRepository.Add(user1);
                //var data= _unitOfWork.RoleRepository.All();
                if (result.Succeeded)
                {
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

        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };
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
    }
}
