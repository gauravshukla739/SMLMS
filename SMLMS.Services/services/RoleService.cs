
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
    public class RoleService : IRoleService
    {
        private IUnitOfWork unitOfWork;
        public RoleService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        #region Roles
        public async Task<ServiceResponse> GetAllRoles()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = unitOfWork.RoleRepository.All();
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ServiceResponse> SaveUpdateRole(RoleDto _role)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                Role role = new Role
                {
                    Id = string.IsNullOrEmpty(_role.Id) ? Guid.NewGuid().ToString() : _role.Id,
                    ConcurrencyStamp = _role.ConcurrencyStamp,
                    Name = _role.Name,
                    NormalizedName = _role.NormalizedName
                };
                if (string.IsNullOrEmpty(_role.Id))
                {
                    unitOfWork.RoleRepository.Add(role);
                }
                else
                {
                    unitOfWork.RoleRepository.Update(role);
                }
                unitOfWork.Commit();
                response.Data = role;
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteRole(string roleId)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var role = unitOfWork.RoleRepository.Find(roleId);
                if (role != null)
                {
                    unitOfWork.RoleRepository.Remove(roleId);
                    response.Data = role;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> GetRoleById(string roleId)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var role = unitOfWork.RoleRepository.Find(roleId);
                if (role != null)
                {
                    response.Data = role;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        #endregion

        #region Modules

        public async Task<ServiceResponse> GetAllModule()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = unitOfWork.ModuleRepository.All();
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ServiceResponse> SaveUpdateModule(ModuleDto model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                Module data = new Module
                {
                    Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid().ToString() : model.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    Description = model.Description,
                    DisplayName = model.DisplayName,
                    IsDeleted = false,
                    Name = model.Name

                };
                if (string.IsNullOrEmpty(model.Id))
                    unitOfWork.ModuleRepository.Add(data);
                else
                    unitOfWork.ModuleRepository.Update(data);
                unitOfWork.Commit();
                response.Data = data;
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteModule(string id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var record = unitOfWork.ModuleRepository.Find(id);
                if (record != null)
                {
                    unitOfWork.ModuleRepository.Remove(id);
                    unitOfWork.Commit();
                    response.Data = record;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> GetModuleById(string id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var record = unitOfWork.ModuleRepository.Find(id);
                if (record != null)
                {
                    response.Data = record;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        #endregion

        #region Role module permission 

        public async Task<ServiceResponse> GetAllRoleModulePermission()
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                response.Data = unitOfWork.RoleModulePermissionRepository.All();
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<ServiceResponse> SaveUpdateRoleModulePermission(RoleModulePermissionDto model)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                RoleModulePermission data = new RoleModulePermission
                {
                    Id = string.IsNullOrEmpty(model.Id) ? Guid.NewGuid().ToString() : model.Id,
                    CreateDate = DateTime.Now,
                    CreatedBy = model.CreatedBy,
                    CanAdd = model.CanAdd,
                    CanDelete = model.CanDelete,
                    CanEdit = model.CanEdit,
                    CanView = model.CanView,
                    ModuleId = model.ModuleId,
                    RoleId = model.RoleId,
                    IsDeleted = false,
                };
                if (string.IsNullOrEmpty(model.Id))
                    unitOfWork.RoleModulePermissionRepository.Add(data);
                else
                    unitOfWork.RoleModulePermissionRepository.Update(data);
                unitOfWork.Commit();
                response.Data = data;
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteRoleModulePermission(string id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var record = unitOfWork.RoleModulePermissionRepository.Find(id);
                if (record != null)
                {
                    unitOfWork.ModuleRepository.Remove(id);
                    unitOfWork.Commit();
                    response.Data = record;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse> GetRoleModulePermissionById(string id)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var record = unitOfWork.RoleModulePermissionRepository.Find(id);
                if (record != null)
                {
                    response.Data = record;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Record Not Found";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.ToString();
            }
            return response;
        }

        #endregion
    }
}
