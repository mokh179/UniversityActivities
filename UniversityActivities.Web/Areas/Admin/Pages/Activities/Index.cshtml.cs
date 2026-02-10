using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class IndexModel : BaseModel
    {
        private readonly IViewAdminActivitiesUseCase _useCase;
        private readonly IUiLookupsQuery _lookupsQuery;
        private readonly IfilterLookupQuery _lookupsfilterQuery;



        public PagedResult<ActivityAdminListItemDto> Result { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        private const int PageSize = 5; // change to 5 if needed


        [BindProperty(SupportsGet = true)]
        public ActivityAdminFilter Filter { get; set; } = new();
        public AdminStatistics adminStatistics { get; private set; }
        public UiLookupsDto Lookups { get; private set; } = new();

        public IndexModel(IViewAdminActivitiesUseCase useCase, IUiLookupsQuery lookupsQuery, IfilterLookupQuery lookupsfilterQuery)
        {
            _useCase = useCase;
            _lookupsQuery = lookupsQuery;
            _lookupsfilterQuery = lookupsfilterQuery;
        }
        public async Task OnGetAsync()
        {
            var request = new PagedRequest() { PageNumber = PageNumber, PageSize = PageSize };
            Lookups = await _lookupsQuery.GetAllAsync();
            if (User.IsInRole("SuperAdmin"))
            {
                // No filter for super admins – get all activities
                Result = await _useCase.ExecuteAsync(null, request);
                adminStatistics = await _useCase.GetAdminStatisticsAsync(null);
                return;
            }
            var filter = new ActivityAdminFilter()
            {
                ManagementId = ManagementId
            };
            // No filter for now – get all activities
            Result = await _useCase.ExecuteAsync(filter, request);
            adminStatistics = await _useCase.GetAdminStatisticsAsync(ManagementId);
        }

        public async Task<IActionResult> OnGetFilterAsync([FromQuery] ActivityAdminFilter filter,int pageNumber = 1)
        {
            var request = new PagedRequest() { PageNumber = PageNumber, PageSize = PageSize };

            var result = await _useCase.ExecuteAsync(filter, request);

            return new JsonResult(result);
        }



        public async Task<JsonResult> OnGetManagementsByTypeAsync(int managementTypeId)
        {
            if (managementTypeId <= 0)
                return new JsonResult(new List<LookupDto>());

            var result = await _lookupsfilterQuery
                .GetByTypeAsync(managementTypeId);

            return new JsonResult(result);
        }

        public async Task<JsonResult> OnGetClubsByManagementAsync(int managementId)
        {
            if (managementId <= 0)
                return new JsonResult(new List<LookupDto>());

            var result = await _lookupsfilterQuery
                .GetByClubAsync(managementId);

            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetDefaultAsync(int pageNumber = 1)
        {
            var request = new PagedRequest() { PageNumber = PageNumber, PageSize = PageSize };

            var result = await _useCase.ExecuteAsync(
                filter: null,   
                request
            );

            return new JsonResult(result);
        }

        [AllowAnonymous]
        public async Task<JsonResult> OnGetParticipantsAsync([FromQuery]int activityId)
        {

            var result = await _useCase.getActivitypaticipants(activityId);

            return new JsonResult(result);
        }
    
    }
}
