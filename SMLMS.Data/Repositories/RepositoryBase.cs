﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal abstract class RepositoryBase
    {
        private IDbTransaction _transaction;
        private IDbConnection Connection { get { return _transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        protected T ExecuteScalar<T>(string sql, object param)
        {
            return Connection.ExecuteScalar<T>(sql, param, _transaction);
        }

        protected T QuerySingleOrDefault<T>(string sql, object param)
        {
            return Connection.QuerySingleOrDefault<T>(sql, param, _transaction);
        }

        protected IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return Connection.Query<T>(sql, param, _transaction);
        }

        protected  void Execute(string sql, object param)
        {
            Connection.Execute(sql, param, _transaction);
        }

        public IEnumerable<T> ExecuteProcedureGetList<T>(string sql, object param)
        {
            return Connection.Query<T>(sql, param, _transaction, commandType: CommandType.StoredProcedure).ToList();
        }
    }
}
