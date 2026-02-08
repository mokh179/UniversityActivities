using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    [Authorize]
    public class IndexModel : BaseModel
    {
        private readonly IViewStudentActivitiesUseCase _viewStudentActivitiesUseCase;
        private readonly IUiLookupsQuery _lookupsQuery;

        public IndexModel(IViewStudentActivitiesUseCase viewStudentActivitiesUseCase,IUiLookupsQuery lookupsQuery)
        {
              _viewStudentActivitiesUseCase = viewStudentActivitiesUseCase;
            _lookupsQuery = lookupsQuery;
        }
        public PagedResult<StudentActivityListItemDto> Result { get; private set; }
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        private const int PageSize = 5; // change to 5 if needed
        [BindProperty(SupportsGet = true)]
        public StudentActivityFilter Filter { get; set; } = new();

        public UiLookupsDto Lookups { get; private set; } = new();

        public int TotalCount { get; set; }
        public int TotalPages =>
            PageSize == 0 ? 1 : (int)Math.Ceiling(TotalCount / (double)PageSize);
        public async Task OnGetAsync()
        {
           
            var request = new PagedRequest() { PageNumber = PageNumber, PageSize = PageSize };
            Lookups = await _lookupsQuery.GetAllAsync();
            Result = await _viewStudentActivitiesUseCase.ExecuteAsync(CurrentUserId, [TargetAudienceId.Value], Filter, request);

        }

        public async Task<IActionResult> OnGetFilterAsync([FromQuery] StudentActivityFilter filter, int pageNumber = 1)
        {
            var request = new PagedRequest() { PageNumber = PageNumber, PageSize = PageSize };

            var result = await _viewStudentActivitiesUseCase.ExecuteAsync(CurrentUserId, [TargetAudienceId.Value], filter, request);

            return new JsonResult(result);
        }
    }


}
