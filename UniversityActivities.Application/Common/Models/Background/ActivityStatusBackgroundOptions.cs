using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Common.Models.Background
{
    public class ActivityStatusBackgroundOptions
    {
        public int IntervalMinutes { get; set; } = 5;
        public bool Enabled { get; set; } = true;
    }
}
