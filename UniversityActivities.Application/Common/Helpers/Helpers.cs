using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Common.Helpers
{
    public class Helpers
    {
        public static int CalculateActivityStatus(DateTime startDate, DateTime endDate)
            {
                if (startDate >= endDate)
                    throw new InvalidOperationException("Activity start date must be before end date.");
    
                if (startDate.Date == DateTime.Now.Date)
                    return 1; // Ongoing
                if (startDate > DateTime.Now)
                    return 3; // Upcoming
                if (endDate < DateTime.Now)
                    return 2; // Past
    
                return 0; // Default/Unknown
        }
    }
}
