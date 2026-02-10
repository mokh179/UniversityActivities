using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;

namespace UniversityActivities.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string NationalId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int ManagementId { get; set; }
        public Management? Management { get; set; }
        public Gender Gender { get; set; }
        public int TargetaudienceId { get; set; }
        public TargetAudience? Targetaudience { get; set; }

    }
}
