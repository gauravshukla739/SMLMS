using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLMS.Data.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        ApplicationUser FindByNormalizedUserName(string normalizedUserName);

        ApplicationUser FindByNormalizedEmail(string normalizedEmail);

        void AddRole(UserRole entity);

        UserRole GetRole(string key);

        int Delete(string key);
    }
}
