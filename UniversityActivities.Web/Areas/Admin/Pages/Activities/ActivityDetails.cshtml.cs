using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class ActivityDetailsModel : BaseModel
    {
        public ActivityAdminDetailsDto? Activity { get; set; } = default!;
        private readonly IAdminViewActivityDetailsUseCase _viewActivityDetailsUseCase;
        private readonly IDeleteActivityUseCase  _activityUseCase;
        public ActivityDetailsModel(IAdminViewActivityDetailsUseCase viewActivityDetailsUseCase,IDeleteActivityUseCase activityUseCase)
        {
                _viewActivityDetailsUseCase = viewActivityDetailsUseCase;   
                _activityUseCase = activityUseCase;
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
    }
}
