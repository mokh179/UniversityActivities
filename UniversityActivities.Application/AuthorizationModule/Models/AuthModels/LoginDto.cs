using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.AuthorizationModule.Models.AuthModels
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
