﻿
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
    public class TaskService : ITaskService
    {
        private IUnitOfWork unitOfWork;
        public TaskService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        #region Task

        public async Task<ServiceResponse> GetAllTasks()
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var data = unitOfWork.TaskRepository.All();
                unitOfWork.Commit();
                _response.IsSuccess = true;
                _response.Message = "Success";
                _response.Data = data;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        public async Task<ServiceResponse> SaveUpdateTask(Model.Core.Task _task)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                unitOfWork.TaskRepository.Add(_task);
                unitOfWork.Commit();
                _response.IsSuccess = true;
                _response.Message = "Task added successfully";

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response; 
        }

        public async Task<ServiceResponse> DeleteTask(string taskId)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                unitOfWork.TaskRepository.Remove(taskId);
                unitOfWork.Commit();
                _response.IsSuccess = true;
                _response.Message = "Task deleted successfully";

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        public async Task<ServiceResponse> GetTaskById(string taskId)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var task=unitOfWork.TaskRepository.Find(taskId);
                unitOfWork.Commit();
                _response.IsSuccess = true;
                _response.Message = "Task fetched successfully";
                _response.Data = task;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        public async Task<ServiceResponse> GetTaskByEmployeeId(Guid id)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var taskList = unitOfWork.TaskRepository.FindByEmployeeId(id);
                unitOfWork.Commit();
                _response.IsSuccess = true;
                _response.Message = "Task fetched successfully";
                _response.Data = taskList;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        public async Task<ServiceResponse> GetTaskByDepartmentId(Guid id)
        {
            ServiceResponse _response = new ServiceResponse();
            try
            {
                var taskList = unitOfWork.TaskRepository.FindByDepartmentId(id);
                unitOfWork.Commit();
                _response.IsSuccess = true;
                _response.Message = "Task fetched successfully";
                _response.Data = taskList;

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        #endregion


    }
}