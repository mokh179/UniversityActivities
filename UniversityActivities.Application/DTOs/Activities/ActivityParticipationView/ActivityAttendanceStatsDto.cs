using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.ActivityParticipationView
{
    public class ActivityAttendanceStatsDto
    {
        public int RegisteredCount { get; set; }
        public int AttendedCount { get; set; }
        public int ParticipatedCount { get; set; }
        public double OverallAverageRate { get; set; }
    }
}
