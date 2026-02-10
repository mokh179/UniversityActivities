using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities
{
    public class AdminStatistics
    {
        public int TotalActivities { get;  set; }
        public int UpcomingActivities { get;  set; }
        public int InProgressActivities { get;  set; }
        public int CompletedActivities { get;  set; }
    }
}
