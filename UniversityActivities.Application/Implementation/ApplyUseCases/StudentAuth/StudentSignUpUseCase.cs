using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.Exceptions;
using UniversityActivities.Application.UserCases.Student.Auth;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.StudentAuth
{
    public class StudentSignUpUseCase : IStudentSignUpUseCase
    {
        private readonly IIdentityMangment _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public StudentSignUpUseCase(
            IIdentityMangment identityService,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<LoginResponseDto> ExecuteAsync(RegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new BusinessException("Password confirmation does not match.");

            // 1️ Create student user
            var userId = await _identityService.CreateUserAsync(dto);

            // 2️ Generate claims
            var (id, userName, claims) =
                await _identityService.GenerateClaimsAsync(userId);

            // 3️ Generate JWT
            var token = _jwtTokenGenerator.GenerateToken(claims);

            return new LoginResponseDto
            {
                UserId = id,
                UserName = userName,
                Token = token
            };
        }
    }
}
