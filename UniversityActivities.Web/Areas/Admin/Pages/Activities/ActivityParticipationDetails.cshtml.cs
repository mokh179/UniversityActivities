using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.DTOs.Activities.ActivityParticipationView;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class ActivityParticipationDetailsModel : BaseModel
    {
        private readonly IAdminActivityDetailsQuery _adminActivityDetailsQuery;
        private readonly IMarkStudentAttendanceUseCase _markStudentAttendanceUseCase;

        public ActivityAttendanceStatsDto ActivityStats { get; set; } = new ActivityAttendanceStatsDto();
        public ActivityParticipantFilter filter { get; set; } = new ActivityParticipantFilter();
        public PagedResult<ActivityParticipantDto> Result { get; private set; }
        public bool CanViewCertificate { get; private set; }
        public StudentEvaluationModalDto student { get; private set; }
        public ActivityParticipationDetailsModel(IAdminActivityDetailsQuery adminActivityDetailsQuery, IMarkStudentAttendanceUseCase markStudentAttendanceUseCase)
        {
                _adminActivityDetailsQuery = adminActivityDetailsQuery;
                _markStudentAttendanceUseCase = markStudentAttendanceUseCase;
        } 
       
        public async Task OnGet(int id)
        {
                ActivityStats = await _adminActivityDetailsQuery.GetActivityStatsAsync(id);
                 filter = new ActivityParticipantFilter() { ActivityId = id, PageNumber = 1, PageSize = 10 };
                Result = await _adminActivityDetailsQuery.GetParticipantsAsync(filter);
               // CanViewCertificate = await _adminActivityDetailsQuery.CanViewCertificateAsync(id);
        }
        public async Task<IActionResult> OnPostAttend(int id,int studentid)
        {
            await _markStudentAttendanceUseCase.ExecuteAsync(studentid, id);

            return new JsonResult(new { success = true, message = "Attented" });
        }
        public async Task<IActionResult> OnGetRate(int id, int studentid)
        {
            var result=await _adminActivityDetailsQuery.GetStudentEvaluationAsync(id, studentid);

            return new JsonResult(new { success = true, result });
        }
    }
}
