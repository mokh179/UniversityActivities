using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Admin
{
    public class AdminDashboardStatsDto
    {
        public int TotalActivities { get; set; }
        public int PublishedActivities { get; set; }
        public int ActiveActivities { get; set; }
        public int TotalStudents { get; set; }
        public int InProgressActivities { get; set; }  

    }
}
