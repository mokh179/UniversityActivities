using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.UserCases.Student.Auth;

namespace UniversityActivities.Application.ApplyUseCases.StudentAuth
{
    public class LogOutUseCase:ILogOutUseCase
    {
        private readonly IIdentityMangment _identityService;

        public LogOutUseCase(IIdentityMangment identityService)
        {
            _identityService = identityService;

        }
        public async Task ExecuteAsync()
        {
            await _identityService.LogoutAsync();
        }
    }
}
