using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Evaluation
{
    public class ActivityEvaluationCriteriaAverageDto
    {
        public int EvaluationCriteriaId { get; set; }

        public string CriteriaNameAr { get; set; } = null!;
        public string CriteriaNameEn { get; set; } = null!;

        public double AverageValue { get; set; }
    }

}
