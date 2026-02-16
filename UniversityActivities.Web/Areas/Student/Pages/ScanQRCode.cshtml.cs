using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityActivities.Application.DTOs.Scan.Models;
using UniversityActivities.Application.UseCases.Activities.Scan;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public class ScanQRCodeModel : PageModel
    {
        private readonly IHandleActivityScanUseCase _scanUseCase;

        public int ActivityId { get; set; }

        public ScanQRCodeModel(IHandleActivityScanUseCase scanUseCase)
        {
            _scanUseCase = scanUseCase;
        }
        //public async Task<IActionResult> OnGetAsync(
        //int activityId,
        //CancellationToken cancellationToken)
        //{
        //    int? userId = null;

        //    if (User.Identity?.IsAuthenticated == true)
        //    {
        //        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        //        if (claim != null)
        //            userId = int.Parse(claim.Value);
        //    }

        //    var result = await _scanUseCase.ExecuteAsync(
        //        activityId,
        //        userId,
        //        cancellationToken);

        //    return HandleScanResult(result);
        //}

        //private IActionResult HandleScanResult(ActivityScanResult result)
        //{
        //    return result.Action switch
        //    {
        //        ScanAction.RedirectToLogin =>
        //            Redirect($"/Login?returnUrl=/Activities/ScanQRCode/{result.ActivityId}"),

        //        ScanAction.RegisterAndAttend =>
        //            Redirect($"/Activities/Success/{result.ActivityId}"),

        //        ScanAction.MarkAttendance =>
        //            Redirect($"/Activities/Success/{result.ActivityId}"),

        //        ScanAction.AlreadyAttended =>
        //            Redirect($"/Activities/Already/{result.ActivityId}"),

        //        ScanAction.RedirectToEvaluation =>
        //            Redirect($"/Student/Evaluate/{result.ActivityId}"),

        //        ScanAction.RedirectToCertificate =>
        //            Redirect($"/Student/Certificate/{result.ActivityId}"),

        //        ScanAction.ActivityNotStarted =>
        //            Redirect($"/Activities/NotStarted/{result.ActivityId}"),

        //        _ =>
        //            Redirect($"/Activities/Closed/{result.ActivityId}")
        //    };
        //}

        public async Task<IActionResult> OnGetProcessAsync(int activityId)
        {
            var userId = GetCurrentUserIdOrNull();

            var result = await _scanUseCase.ExecuteAsync(
                activityId,
                userId,
                CancellationToken.None);

            return new JsonResult(result);
        }

        private int? GetCurrentUserIdOrNull()
        {
            if (!User.Identity!.IsAuthenticated)
                return null;

            return int.Parse(User.FindFirst("uid")!.Value);
        }
    }
}
