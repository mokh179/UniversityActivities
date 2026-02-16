using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Clubs
{
    public class CreateOrUpdateClubDto
    {
        public int? Id { get; set; }

        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public string DescAr { get; set; } = string.Empty;
        public string DescEn { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int ClubDomainId { get; set; }

        public int ManagementId { get; set; }

        public int AttendanceScopeId { get; set; }

        public bool IsActive { get; set; }
    }
}
