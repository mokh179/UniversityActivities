using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.DTOs.Evaluation;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Infrastructure.Lookup;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    [IgnoreAntiforgeryToken]

    public class ActivityDetailsModel : BaseModel
    {
        public StudentActivityDetailsDto Activity { get; set; } = null!;
        private readonly IViewStudentActivitiesUseCase _viewStudentActivitiesUseCase;
        private readonly IRegisterStudentInActivityUseCase _registerStudentInActivityUseCase;
        private readonly ISubmitActivityEvaluationUseCase _studentActivityEvaluation;
        private readonly IMarkStudentAttendanceUseCase _markStudentAttendanceUseCase;

        public ActivityDetailsModel(IMarkStudentAttendanceUseCase markStudentAttendanceUseCase, IViewStudentActivitiesUseCase viewStudentActivitiesUseCase, ISubmitActivityEvaluationUseCase studentActivityEvaluation, IRegisterStudentInActivityUseCase registerStudentInActivityUseCase, IUiLookupsQuery lookupsQuery)
        {
            _viewStudentActivitiesUseCase = viewStudentActivitiesUseCase;
            _registerStudentInActivityUseCase = registerStudentInActivityUseCase;
            _studentActivityEvaluation = studentActivityEvaluation;
            _markStudentAttendanceUseCase = markStudentAttendanceUseCase;
        }
   
        public async Task OnGet(int id)
        {
            Activity=await _viewStudentActivitiesUseCase.ExecuteAsync(id, CurrentUserId);
        }
        public async Task<IActionResult> OnGetRegisterAsync(int id)
        {
            await _registerStudentInActivityUseCase.ExecuteAsync(CurrentUserId, id);
            return new JsonResult(new { success = true, message = "Registered successfully" }); 
        }

        public async Task<IActionResult> OnPostRateAsync(int id,[FromBody] List<EvaluationItemDto> dto,string comment)
        {
            // 🔴 Validation حقيقي
            if (dto == null || dto.Count == 0)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "No ratings submitted"
                });
            }
            var RateDto = new SubmitActivityEvaluationDto
            {
                ActivityId = id,
                Items = dto,
                Comment = comment
            };  
            await _studentActivityEvaluation.ExecuteAsync(CurrentUserId,id, RateDto);
            return new JsonResult(new { success = true });
        }


        public async Task<IActionResult> OnGetAttendAsync(int id)
        {
            await _markStudentAttendanceUseCase.ExecuteAsync(CurrentUserId, id);
            return new JsonResult(new { success = true, message = "Attended successfully" });
        }

    }
}
