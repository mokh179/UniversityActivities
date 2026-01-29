using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Evaluation
{
    public class SubmitActivityEvaluationDto
    {
        public int ActivityId { get; set; }
        public List<EvaluationItemDto> Items { get; set; } = new();
        public string? Comment { get; set; }
    }
}
