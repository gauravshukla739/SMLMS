
using SMLMS.Data.Interfaces;
using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<ServiceResponse> GetAll()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = unitOfWork.UserRepository.All();
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
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
                var _uRole = unitOfWork.UserRepository.GetRole(userRole.UserId);
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
