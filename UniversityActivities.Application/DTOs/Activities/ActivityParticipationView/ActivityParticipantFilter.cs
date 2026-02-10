using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.ActivityParticipationView
{
    public class ActivityParticipantFilter
    {
        public int ActivityId { get; set; }

        public string? Search { get; set; }
        public int? TargetAudienceId { get; set; }

        public bool? IsAttended { get; set; }     
        public bool? HasEvaluated { get; set; }   

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
