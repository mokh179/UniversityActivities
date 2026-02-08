using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Infrastructure.Lookup;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    [IgnoreAntiforgeryToken]
    public class EditActivityModel : PageModel
    {

        public int id { get; set; }
        [BindProperty]
        public CreateOrUpdateActivityDto Input { get; set; } = new CreateOrUpdateActivityDto();
        public UiLookupsDto Lookups { get; private set; } = new();

        private readonly IUpdateActivityUseCase _EditUseCase;
        private readonly IUiLookupsQuery _lookupsQuery;
        private readonly IfilterLookupQuery _lookupsfilterQuery;
        private readonly IGetUserManagmentQuery _getUserManagmentQuery;
        public EditActivityModel(IUpdateActivityUseCase EditUseCase
                                , IUiLookupsQuery lookupsQuery,
                                 IfilterLookupQuery lookupsfilterQuery,
                                 IGetUserManagmentQuery getUserManagmentQuery)
        {
            _EditUseCase = EditUseCase;
            _lookupsQuery = lookupsQuery;
            _lookupsfilterQuery = lookupsfilterQuery;
            _getUserManagmentQuery = getUserManagmentQuery;
        }
        public async Task OnGetAsync(int id)
        {
            Input = await _EditUseCase.GetDetailsAsync(id);
            Input.ManagementTypeId=3;
            Lookups = await _lookupsQuery.GetAllAsync();
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
        public async Task<JsonResult> OnGetUsersByManagementAsync(int managementId)
        {
            if (managementId <= 0)
                return new JsonResult(new List<LookupDto>());

            var result = await _getUserManagmentQuery
                .GetUsersinManagement(managementId);

            return new JsonResult(result);
        }
    }
}
