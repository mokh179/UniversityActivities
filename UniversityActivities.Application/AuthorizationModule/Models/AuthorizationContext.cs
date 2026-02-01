using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.AuthorizationModule.Models
{
    public class AuthorizationContext
    {
        public int? ManagementId { get; set; }
        public int? ActivityId { get; set; }
    }
}
