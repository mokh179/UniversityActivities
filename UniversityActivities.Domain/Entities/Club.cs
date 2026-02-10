using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Domain.Entities
{
    public class Club
    {
        public int Id { get; set; }

        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int ClubDomainId { get; set; }
        public ClubDomain ClubDomain { get; set; } = null!;
        public int ManagementId { get; set; }
        public Management Management { get; set; } = null!;

        public int AttendanceScopeId { get; set; }
        public AttendanceScope AttendanceScope { get; set; } = null!;

        // Navigation
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
