using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.AuthorizationModule.Services.Interfaces
{
    public interface IIdentityRoleMangment
    {
        /// <summary>
        /// Ensures that the given user has the specified system role.
        /// If the user already has the role, the operation is ignored.
        /// </summary>
        Task EnsureUserInRoleAsync(int userId, string roleName);
    }
}
