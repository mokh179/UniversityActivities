using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.ActivityParticipationView
{
    public class ActivityParticipantDto
    {
        public int StudentId { get; set; }
        public string StudentFullName { get; set; } = string.Empty;
        public string ManagementName { get; set; } = string.Empty;
        public string ManagementNameAr { get; set; } = string.Empty;

        public bool IsAttended { get; set; }
        public bool HasEvaluated { get; set; }

        public double? StudentAverageRate { get; set; }

    }
}
