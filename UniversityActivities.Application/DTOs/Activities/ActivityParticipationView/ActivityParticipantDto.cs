using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;

namespace UniversityActivities.Application.DTOs.Activities.ActivityParticipationView
{
    public class ActivityParticipantDto
    {
        public int StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string ManagementName { get; set; } = string.Empty;
        public string ManagementNameAr { get; set; } = string.Empty;
        public string TargetAudienceName { get; set; } = string.Empty;
        public string TargetAudienceNameAR { get; set; } = string.Empty;
        public Gender Gender { get; set; } 
        public string NationalId { get; set; } = string.Empty;

        public bool IsAttended { get; set; }
        public bool HasEvaluated { get; set; }

        public double? StudentAverageRate { get; set; }

    }
}
