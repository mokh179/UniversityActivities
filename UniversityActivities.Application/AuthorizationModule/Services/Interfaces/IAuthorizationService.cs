using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;

namespace UniversityActivities.Application.AuthorizationModule.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task EnsureAuthorizedAsync(
        int userId,
        AuthorizationAction action,
        AuthorizationContext context);
    }
}
