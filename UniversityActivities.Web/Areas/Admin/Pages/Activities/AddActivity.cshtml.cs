using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class AddActivityModel : BaseModel
    {
        private readonly ICreateActivityUseCase _createUseCase;
        private readonly IUiLookupsQuery _lookupsQuery;
        private readonly IfilterLookupQuery _lookupsfilterQuery;
        private readonly IGetUserManagmentQuery _getUserManagmentQuery;
        [BindProperty]

        public string AssignmentsJson { get; set; } = string.Empty;


        [BindProperty]
        public CreateOrUpdateActivityDto Input { get; set; } = new();

        public UiLookupsDto Lookups { get; private set; } = new();
        //public ManagementUsersDto Lookups { get; private set; } = new();

        public AddActivityModel(
            ICreateActivityUseCase createUseCase,
            IUiLookupsQuery lookupsQuery,
            IfilterLookupQuery lookupsfilterQuery,
            IGetUserManagmentQuery getUserManagmentQuery)
        {
            _createUseCase = createUseCase;
            _lookupsQuery = lookupsQuery;
            _lookupsfilterQuery = lookupsfilterQuery;
            _getUserManagmentQuery = getUserManagmentQuery;
        }
        public async Task OnGet()
        {
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
