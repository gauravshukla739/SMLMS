using Dapper;
using SMLMS.Data.Interfaces;
using SMLMS.Model.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class LeaveRepositry : RepositoryBase, ILeaveRepositry
    {
        public LeaveRepositry(IDbTransaction transaction)
            : base(transaction)
        { }
        public void Add(LeaveDto entity)
        {
            int flag = 0;
            if (entity.ID != null)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", flag, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", entity.ID, DbType.Guid, ParameterDirection.Input);
            param.Add("@name", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@count", entity.Count, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", 1, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", 1, DbType.Byte, ParameterDirection.Input);
            ExecuteSP("sp_insert_update", param);
        }
        
        public void UpdateData(LeaveDto entity, string id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 1, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", id, DbType.String, ParameterDirection.Input);
            param.Add("@name", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@count", entity.Count, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", 1, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", 1, DbType.Byte, ParameterDirection.Input);
            ExecuteSP("sp_insert_update", param);
        }
        public void Update(LeaveDto entity)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 1, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", null, DbType.String, ParameterDirection.Input);
            param.Add("@name", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@count", entity.Count, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", 1, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", 1, DbType.Byte, ParameterDirection.Input);
            ExecuteSP("sp_insert_update", param);
        }
        public IEnumerable<LeaveDto> All()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 0, DbType.Int32);
            param.Add("@loggedInId", null, DbType.Guid);
            IEnumerable<LeaveDto> result = ExecuteProcedureGetList<LeaveDto>("sp_getleave", param);
            return result;
        }
        public LeaveDto Find(string key)
        {
            return QuerySingleOrDefault<LeaveDto>(
                sql: "SELECT * FROM AspNetRoles WHERE Id = @key",
                param: new { key }
            );
        }
        public void Remove(string key)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 2, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", key, DbType.String, ParameterDirection.Input);
            param.Add("@name", "", DbType.String, ParameterDirection.Input);
            param.Add("@count", null, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", null, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", null, DbType.Byte, ParameterDirection.Input);
            ExecuteSP("sp_insert_update", param);
        }
        public void RemoveRequest(string key)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 3, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", key, DbType.String, ParameterDirection.Input);
            param.Add("@name", "", DbType.String, ParameterDirection.Input);
            param.Add("@count", null, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", null, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", null, DbType.Byte, ParameterDirection.Input);
            ExecuteSP("sp_insert_update", param);
        }
        
        public void RequestLeave(RequestLeave entity)
        {
            int flag = 0;
            if (entity.Id != null)
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", flag, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", entity.Id, DbType.Guid, ParameterDirection.Input);
            param.Add("@LeaveTypeName", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", entity.FromDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@TodateDate", entity.ToDate, DbType.DateTime, ParameterDirection.Input);
            param.Add("@Reason", entity.Reason, DbType.String, ParameterDirection.Input);
            param.Add("@CreatedBy", entity.CreatedBy, DbType.String, ParameterDirection.Input);
            param.Add("@Userid", entity.Userid, DbType.Guid, ParameterDirection.Input);
            ExecuteSP("sp_RequestLeave", param);
        }
        public IEnumerable<RequestLeave> GetLeaveRequest(Guid id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 1, DbType.Int32);
            param.Add("@loggedInId", id, DbType.Guid);
            IEnumerable<RequestLeave> result = ExecuteProcedureGetList<RequestLeave>("sp_getleave", param);
            return result;
        }
        public IEnumerable<RequestLeave> GetDataBasedOnId(Guid deptid, string RoleName)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@deptid", deptid, DbType.Guid, ParameterDirection.Input);
            param.Add("@userchk", RoleName, DbType.String, ParameterDirection.Input);
            IEnumerable<RequestLeave> result = ExecuteProcedureGetList<RequestLeave>("getrolebasedonid", param);
            return result;
        }
        
        public void ApproveLeaveRequest(RequestLeave requestLeave)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@leaveid", requestLeave.Id, DbType.Guid, ParameterDirection.Input);
            param.Add("@approveby", requestLeave.UpdatedBy, DbType.String, ParameterDirection.Input);
            ExecuteSP("sp_approve_leave", param);
        }

    }
}
