using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SMLMS.Model.Core;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMLMS.Services
{
    //public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    //{
    //    public MyUserClaimsPrincipalFactory(
    //        UserManager<ApplicationUser> userManager,
    //        IOptions<IdentityOptions> optionsAccessor)
    //            : base(userManager, optionsAccessor)
    //    {
    //    }

    //    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    //    {
    //        var identity = await base.GenerateClaimsAsync(user);
    //        identity.AddClaim(new Claim("ContactName", user.FirstName ?? "[Click to edit profile]"));
    //        return identity;
    //    }
    //}
}
