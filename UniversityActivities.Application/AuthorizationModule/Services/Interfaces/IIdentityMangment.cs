using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;

namespace UniversityActivities.Application.AuthorizationModule.Services.Interfaces
{
    public interface IIdentityMangment
    {

        Task<int> CreateUserAsync(RegisterDto registerDto);
        /// <summary>
        /// Ensures that the given user has the specified system role.
        /// If the user already has the role, the operation is ignored.
        /// </summary>
        Task EnsureUserInRoleAsync(int userId, string roleName);

        Task<(int UserId, string UserName, List<Claim> Claims)> GenerateClaimsAsync(int userId);

        Task<(int UserId, string UserName)>LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
