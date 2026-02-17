using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.UserCases.Student.Auth;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.StudentAuth
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IIdentityMangment _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUseCase(
            IIdentityMangment identityService,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<LoginResponseDto> ExecuteAsync(LoginDto dto)
        {
            var (userId, user,IsAdmin) =await _identityService.LoginAsync(dto);
            var (id, userName, claims) =
                await _identityService.GenerateClaimsAsync(userId);
            var token = _jwtTokenGenerator.GenerateToken(claims);
            return new LoginResponseDto
            {
                UserId = userId,
                UserName = userName,
                Token = token,
                IsAdmin= IsAdmin
            };
        }
    }
}
