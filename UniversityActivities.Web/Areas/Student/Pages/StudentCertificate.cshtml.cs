using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    public class StudentCertificateModel : BaseModel
    {
        [BindProperty]
       public AttendanceCertificateDto CertificateDto { get; set; } = null!;

        private IGenerateAttendanceCertificateUseCase _generateAttendanceCertificateUseCase;
        public StudentCertificateModel(IGenerateAttendanceCertificateUseCase generateAttendanceCertificateUseCase)
        {
            _generateAttendanceCertificateUseCase = generateAttendanceCertificateUseCase;
        }

        //public string StudentName { get; set; } = null!;
        //public string ActivityTitle { get; set; } = null!;
        //public string ActivityDate { get; set; } = null!;
        //public string Organization { get; set; } = null!;

        public async Task  OnGet(int activityId)
        {
            CertificateDto = await _generateAttendanceCertificateUseCase.ExecuteAsync(CurrentUserId, activityId);
        }
      
    }
}
