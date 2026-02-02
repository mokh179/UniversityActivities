using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.Student
{
    public class AttendanceCertificateDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string ActivityTitle { get; set; } = string.Empty;
        public string ManagementName { get; set; } = string.Empty;

        public string ActivityDate { get; set; } = string.Empty;
        public DateTime IssuedAt { get; set; }

        public string CertificateNumber { get; set; } = string.Empty;
    }
}
