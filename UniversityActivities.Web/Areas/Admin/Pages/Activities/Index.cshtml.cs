using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class IndexModel : BaseModel
    {
        private readonly IViewAdminActivitiesUseCase _useCase;



        public PagedResult<ActivityAdminListItemDto> Result { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        private const int PageSize = 5; // change to 5 if needed


        [BindProperty(SupportsGet = true)]
        public ActivityAdminFilter Filter { get; set; } = new();
        public AdminStatistics adminStatistics { get; private set; }
        public IndexModel(IViewAdminActivitiesUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task OnGetAsync()
        {
            var request = new PagedRequest() { PageNumber=PageNumber,PageSize= PageSize };

            if(User.IsInRole("SuperAdmin"))
            {
                // No filter for super admins – get all activities
                Result = await _useCase.ExecuteAsync(null, request);
                adminStatistics= await _useCase.GetAdminStatisticsAsync(null);
                return ;
            }
            var filter = new ActivityAdminFilter()
            {
                ManagementId = ManagementId
            };
            // No filter for now – get all activities
            Result = await _useCase.ExecuteAsync(filter, request);
            adminStatistics = await _useCase.GetAdminStatisticsAsync(ManagementId);
        }

        public async Task<PartialViewResult> OnGetFilterAsync(
ActivityAdminFilter filter,
int pageNumber = 1)
        {
            var request = new PagedRequest() { PageNumber = PageNumber, PageSize = PageSize };

            var result = await _useCase.ExecuteAsync(filter, request);

            return Partial("_ActivitiesTable", result);
        }
    }
}
