using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace UniversityActivities.Application.AuthorizationModule.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(List<Claim> claims);
    }
}
