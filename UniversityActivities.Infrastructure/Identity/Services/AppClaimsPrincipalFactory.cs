using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace UniversityActivities.Infrastructure.Identity.Services
{
    public class AppClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> options)
            : base(userManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(
            ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (user.ManagementId > 0) 
            {
                identity.AddClaim(
                    new Claim("Management_id", user.ManagementId.ToString()!));
            }

            identity.AddClaim(
                new Claim("Gender", user.Gender.ToString()));

            identity.AddClaim(
                new Claim("TargetAudience", user.TargetaudienceId.ToString()));

            return identity;
        }
    }
}
