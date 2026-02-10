using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.ActivityParticipationView
{
    public class StudentEvaluationCriteriaDto
    {
        public string CriteriaName { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
