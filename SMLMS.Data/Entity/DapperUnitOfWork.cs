using SMLMS.Data.Interfaces;
using SMLMS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SMLMS.Data.Entity
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        #region Fields
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        public IRoleRepository _roleRepository;
        public IModuleRepository _moduleRepository;
        public IRoleModulePermissionRepository _roleModulePermissionRepository;
        public IDepartmentRepository _departmentRepository;
        public IAttendanceRepository _attendanceRepository;

        private bool _disposed;
        #endregion

        public DapperUnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        #region IUnitOfWork Members


        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository
                    ?? (_userRepository = new UserRepository(_transaction));
            }
        }

        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                return _userRoleRepository
                    ?? (_userRoleRepository = new UserRoleRepository(_transaction));
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                return _roleRepository
                    ?? (_roleRepository = new RoleRepository(_transaction));
            }
        }

        public IModuleRepository ModuleRepository
        {
            get
            {
                return _moduleRepository
                    ?? (_moduleRepository = new ModuleRepository(_transaction));
            }
        }

        public IRoleModulePermissionRepository RoleModulePermissionRepository
        {
            get
            {
                return _roleModulePermissionRepository
                    ?? (_roleModulePermissionRepository = new RoleModulePermissionRepository(_transaction));
            }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return _departmentRepository
                    ?? (_departmentRepository = new DepartmentRepository(_transaction));
            }
        }

        public IAttendanceRepository AttendanceRepository 
        {
            get
            {
                return _attendanceRepository
                    ?? (_attendanceRepository = new AttendanceRepository(_transaction));
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                resetRepositories();
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Private Methods
        private void resetRepositories()
        {
            _userRepository = null;
            _departmentRepository = null;
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~DapperUnitOfWork()
        {
            dispose(false);
        }
        #endregion
    }
}
