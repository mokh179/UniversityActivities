using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.QRCode;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    [IgnoreAntiforgeryTokenAttribute]

    public class ActivityDetailsModel : BaseModel
    {
        public ActivityAdminDetailsDto? Activity { get; set; } = default!;
        private readonly IAdminViewActivityDetailsUseCase _viewActivityDetailsUseCase;
        private readonly IDeleteActivityUseCase  _activityUseCase;
        private readonly IQRCodeGeneratorService _qrcodegenerator;
        public ActivityDetailsModel(IQRCodeGeneratorService qrcodegenerator,IAdminViewActivityDetailsUseCase viewActivityDetailsUseCase,IDeleteActivityUseCase activityUseCase)
        {
                _viewActivityDetailsUseCase = viewActivityDetailsUseCase;   
                _activityUseCase = activityUseCase;
            _qrcodegenerator = qrcodegenerator;
        }
        public async Task OnGet(int id)
        {
            Activity = await _viewActivityDetailsUseCase.ExecuteAsync(id);
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
           await _activityUseCase.ExecuteAsync(id);
 
            return RedirectToPage("/Activities/Index");
        }
        public IActionResult OnGetGenerateQR(int activityId)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var fullUrl = $"{baseUrl}/student/ActivityDetails/scanqrcode/{activityId}";

            var qrBytes = _qrcodegenerator.GenerateQRCode(fullUrl);

            return File(qrBytes, "image/png");
        }
    }
}
