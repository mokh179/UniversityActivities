using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;

namespace UniversityActivities.Application.UserCases.Student.Auth
{
    public interface IStudentSignUpUseCase
    {
        Task<LoginResponseDto> ExecuteAsync(RegisterDto dto);
    }
}
