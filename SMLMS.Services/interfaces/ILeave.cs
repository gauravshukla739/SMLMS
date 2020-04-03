﻿using SMLMS.Helper.ServiceResponse;
using SMLMS.Model.Core;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services.interfaces
{
    public interface ILeave
    {
        Task<ServiceResponse> GetLeaveType();
        Task<ServiceResponse> PostLeave(LeaveDto model);
        Task<ServiceResponse> UpdateLeave(LeaveDto model, string id);
        Task<ServiceResponse> DeleteLeave(string id);

        Task<ServiceResponse> RequestLeave(RequestLeave model,string id);
        Task<ServiceResponse> GetLeaveRequest();
        
    }
}