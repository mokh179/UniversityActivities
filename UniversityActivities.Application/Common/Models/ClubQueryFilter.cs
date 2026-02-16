using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Common.Models
{
    public class ClubQueryFilter
    {
        public int? ManagementId { get; set; }
        public int? DomainId { get; set; }
        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
