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


            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 0, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", null, DbType.String, ParameterDirection.Input);
            param.Add("@name", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@count", entity.Count, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", 1, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", 1, DbType.Byte, ParameterDirection.Input);
            Execute("sp_insert_update", param);
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
            Execute("sp_insert_update", param);
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
            Execute("sp_insert_update", param);
        }
        //public IEnumerable<LeaveDto> All()
        //{
        //    return Query<LeaveDto>(
        //        sql: "select * from LeaveType"
        //    );
        //}
        public IEnumerable<LeaveDto> All()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 0, DbType.Int32);
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
            param.Add("@name", null, DbType.String, ParameterDirection.Input);
            param.Add("@count", null, DbType.Int32, ParameterDirection.Input);
            param.Add("@UpdatedBy", null, DbType.Byte, ParameterDirection.Input);
            param.Add("@CreatedBy", null, DbType.Byte, ParameterDirection.Input);
            Execute("sp_insert_update", param);
        }
        public void RequestLeave(RequestLeave entity)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 0, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", null, DbType.String, ParameterDirection.Input);
            param.Add("@LeaveTypeName", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", null, DbType.DateTime, ParameterDirection.Input);
            param.Add("@TodateDate", null, DbType.DateTime, ParameterDirection.Input);
            param.Add("@DayBegin", entity.DayBegin, DbType.String, ParameterDirection.Input);
            param.Add("@DayEnd", entity.DayEnd, DbType.String, ParameterDirection.Input);
            param.Add("@LeaveFrom", entity.ShortLeaveFrom, DbType.String, ParameterDirection.Input);
            param.Add("@LeaveTo", entity.ShortLeaveTo, DbType.String, ParameterDirection.Input);
            param.Add("@Reason", entity.Reason, DbType.String, ParameterDirection.Input);
            Execute("sp_RequestLeave", param);
        }
        public void UpdateRequestLeave(RequestLeave entity, string id)
        {


            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 1, DbType.Int32, ParameterDirection.Input);
            param.Add("@id", id, DbType.String, ParameterDirection.Input);
            param.Add("@LeaveTypeName", entity.Name, DbType.String, ParameterDirection.Input);
            param.Add("@FromDate", null, DbType.DateTime, ParameterDirection.Input);
            param.Add("@TodateDate", null, DbType.DateTime, ParameterDirection.Input);
            param.Add("@DayBegin", entity.DayBegin, DbType.String, ParameterDirection.Input);
            param.Add("@DayEnd", entity.DayEnd, DbType.String, ParameterDirection.Input);
            param.Add("@LeaveFrom", entity.ShortLeaveFrom, DbType.String, ParameterDirection.Input);
            param.Add("@LeaveTo", entity.ShortLeaveTo, DbType.String, ParameterDirection.Input);
            param.Add("@Reason", entity.Reason, DbType.String, ParameterDirection.Input);
            Execute("sp_RequestLeave", param);
        }

        //public Leave FindByName(string roleName)
        //{
        //    return QuerySingleOrDefault<Leave>(
        //        sql: "SELECT * FROM AspNetRoles WHERE [Name] = @roleName",
        //        param: new { roleName }
        //    );
        //}

        public IEnumerable<RequestLeave> GetLeaveRequest()
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@flag", 1, DbType.Int32);
            IEnumerable<RequestLeave> result =  ExecuteProcedureGetList<RequestLeave>("sp_getleave", param);
            return result;
        }
    }
}
