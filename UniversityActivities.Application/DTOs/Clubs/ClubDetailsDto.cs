using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Clubs
{
    public class ClubDetailsDto
    {
        public int Id { get; set; }

        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public string DescAr { get; set; } = string.Empty;
        public string DescEn { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int ClubDomainId { get; set; }
        public string ClubDomainNameAr { get; set; } = string.Empty;
        public string ClubDomainNameEn { get; set; } = string.Empty;

        public int ManagementId { get; set; }
        public string ManagementNameAr { get; set; } = string.Empty;
        public string ManagementNameEn { get; set; } = string.Empty;

        //public int AttendanceScopeId { get; set; }
        //public string AttendanceScopeNameAr { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
