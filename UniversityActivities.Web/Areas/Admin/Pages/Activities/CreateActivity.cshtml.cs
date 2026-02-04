using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.Json;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class ActivityAssignmentRequestDto
    {
        public int RoleId { get; set; }
        public int ActivityId { get; set; }
        public List<int> UserIds { get; set; } = new();
    }
    public class CreateActivityModel : BaseModel
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

        public CreateActivityModel(
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
        public async Task OnGetAsync()
        {
            Lookups = await _lookupsQuery.GetAllAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var data = JsonSerializer.Deserialize<List<ActivityAssignmentRequestDto>>(
                            AssignmentsJson,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
            Input.Assignments= data.SelectMany(x => x.UserIds.Select(userId => new ActivityAssignmentDto{UserId = userId,ActivityRoleId = x.RoleId})).ToList();
            if (!ModelState.IsValid)
            {
                Lookups = await _lookupsQuery.GetAllAsync();
                return Page();
            }

            var managementId = User.IsInRole("SuperAdmin")
                ? 0
                : int.Parse(User.FindFirst("management_id")!.Value);

            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _createUseCase.ExecuteAsync(
                Input);

            return RedirectToPage("Index");
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
