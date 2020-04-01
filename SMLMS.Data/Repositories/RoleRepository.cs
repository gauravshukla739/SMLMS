using Dapper;
using SMLMS.Data.Interfaces;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLMS.Data.Repositories
{
    internal class RoleRepository : RepositoryBase, IRoleRepository
    {
        public RoleRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public void Add(Role entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", entity.Id);
            parameters.Add("@Name", entity.Name);
            parameters.Add("@NormalizedName", entity.NormalizedName);
            parameters.Add("@ConcurrencyStamp",entity.ConcurrencyStamp);
            Execute(
                sql: @"INSERT INTO AspNetRoles(Id, ConcurrencyStamp, [Name], NormalizedName)
                    VALUES(@Id, @ConcurrencyStamp, @Name, @NormalizedName)",
               param: parameters
            );
        }

        public IEnumerable<Role> All()
        {
            return Query<Role>(
                sql: "SELECT * FROM AspNetRoles"
            );
        }

        public Role Find(string key)
        {
            return QuerySingleOrDefault<Role>(
                sql: "SELECT * FROM AspNetRoles WHERE Id = @key",
                param: new { key }
            );
        }

        public Role FindByName(string roleName)
        {
            return QuerySingleOrDefault<Role>(
                sql: "SELECT * FROM AspNetRoles WHERE [Name] = @roleName",
                param: new { roleName }
            );
        }


        public void Remove(string key)
        {
            Execute(
                sql: "DELETE FROM AspNetRoles WHERE Id = @key",
                param: new { key }
            );

            throw new NotImplementedException();
        }

        public void Update(Role entity)
        {
            Execute(
                sql: @"
                    UPDATE AspNetRoles SET ConcurrencyStamp = @ConcurrencyStamp,
                        [Name] = @Name, NormalizedName = @NormalizedName
                    WHERE Id = @Id",
                param: entity
            );
        }
    }
}
