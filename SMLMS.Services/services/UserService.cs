
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUnitOfWork _unitOfWork, IAuthenticationService authenticationService, UserManager<ApplicationUser> userManager)
        {
            unitOfWork = _unitOfWork;
            _authenticationService = authenticationService;
            _userManager = userManager;
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                 var data=  unitOfWork.UserRepository.GetAll();
                 data.ToList().ForEach(x => x.ImageData= ViewImage(x.Image));
                response.Data = data;
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }
            return response;
        }
        public string ViewImage(byte[] image)
        {
            string src = string.Empty;
            if (image != null)
            {
                string base64String = Convert.ToBase64String(image, 0, image.Length);
                src= "data:image/png;base64," + base64String;
            }
            return src;
        }

        public async Task<ServiceResponse> AddUserRole(UserRoleDto _userRole)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                UserRole userRole = new UserRole
                {
                    UserId = _userRole.UserId,
                    RoleId = _userRole.RoleId
                };
                var _uRole = unitOfWork.UserRepository.GetRole(userRole.UserId.ToString());
                if (_uRole == null)
                {
                    unitOfWork.UserRepository.AddRole(userRole);
                    unitOfWork.Commit();
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = $"User already has assign a role";
                    response.IsSuccess = false;
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ServiceResponse> Delete(string userId)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                 unitOfWork.UserRepository.Delete(userId);
                 unitOfWork.Commit();              
                    response.IsSuccess = true;            
                    response.Data = "Remove Successfully!";
                
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }


        public async Task<ServiceResponse> Import(List<UserDto> user,ClaimsPrincipal claims,string type)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                int i = 0;
                var count = user.Count;
                response.IsSuccess = true;
                foreach (var item in user)
                {
                    
                    var dept = unitOfWork.DepartmentRepository.Find(item.DepartmentName);
                    item.DepartmentId = dept.Id;
                    var result= await _authenticationService.CreateUser(item, claims,type);
                    if (!result.IsSuccess)
                    {
                        i++;
                    }
                    else
                    {
                        unitOfWork.UserRoleRepository.UpdateDepartment(result.Data.ToString(), item.DepartmentId.ToString());
                        
                    }
                   
                }
                unitOfWork.Commit();
                if (i == 0)
                {
                    response.Message = "All record inserted successfully!";
                }
                else if (i == count)
                {
                    response.IsSuccess = false;
                    response.Message = "Error in Saving data";
                }
                else
                {
                    response.Message = $"Only {count - i} record inserted successfully!";
                }


            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> UploadImage(IFormFile file, ClaimsPrincipal claims)
        {
            ServiceResponse response = new ServiceResponse() { IsSuccess = true };
            try
            {
                var userId = claims.Claims.First(x => x.Type == "UserId").Value;
                var email = claims.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                Byte[] bytes = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.OpenReadStream().CopyTo(ms);
                        bytes = ms.ToArray();
                    }

                 unitOfWork.UserRepository.UpdateImage(userId, bytes, email);
                unitOfWork.Commit();
                response.Message = "Image Uploaded Successfully!";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

      
        public async Task<ServiceResponse> Promote(UserDto user, ClaimsPrincipal claims)
        {
            ServiceResponse response = new ServiceResponse() { IsSuccess = true};
            try
            {
                unitOfWork.UserRoleRepository.Remove(user.Id.ToString());
                var userDetail = await _userManager.FindByIdAsync(user.Id.ToString());
                await _userManager.AddToRoleAsync(userDetail, user.RoleName);
                unitOfWork.UserRoleRepository.UpdateDepartment(user.Id.ToString(), user.DepartmentId.ToString());
                unitOfWork.Commit();
                response.Message = "User is Promoted Successfully!";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse> GetRole(string userId)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = unitOfWork.UserRepository.GetRole(userId);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
