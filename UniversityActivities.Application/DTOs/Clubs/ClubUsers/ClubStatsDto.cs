using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Clubs.ClubUsers
{
    public class ClubStatsDto
    {
        public int TotalMembers { get; set; }

        public int PendingRequests { get; set; }

        public int RejectedRequests { get; set; }
    }
}
