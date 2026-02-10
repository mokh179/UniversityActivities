using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.ActivityParticipationView
{
    public class StudentEvaluationModalDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string? Comment { get; set; }

        public List<StudentEvaluationCriteriaDto> Criteria { get; set; }
            = new();
    }
}
