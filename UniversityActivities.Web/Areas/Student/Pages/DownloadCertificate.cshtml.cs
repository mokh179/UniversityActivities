using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    public class DownloadCertificateModel : BaseModel
    {
        AttendanceCertificateDto CertificateDto = new AttendanceCertificateDto();

        private IGenerateAttendanceCertificateUseCase _generateAttendanceCertificateUseCase;
        public DownloadCertificateModel(IGenerateAttendanceCertificateUseCase generateAttendanceCertificateUseCase)
        {
                _generateAttendanceCertificateUseCase = generateAttendanceCertificateUseCase;   
        }
        public async Task<IActionResult> OnGet(int activityId)
        {


            CertificateDto = await  _generateAttendanceCertificateUseCase.ExecuteAsync(CurrentUserId, activityId);

            var pdfBytes = GenerateCertificatePdf(CertificateDto);

            return File(
                pdfBytes,
                "application/pdf",
                $"Certificate_{CertificateDto.ActivityTitle}.pdf"
            );
        }

        private byte[] GenerateCertificatePdf(AttendanceCertificateDto data)
        {
            var document = new CertificatePdfDocument(data);

            return document.GeneratePdf();
        }
    }
}
