using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.AuthorizationModule.Models.AuthModels
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
