using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.DTOs.Admin;
using UniversityActivities.Application.Interfaces.Repositories.Admin;
using UniversityActivities.Application.UserCases.Admin;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages
{
    [Authorize(Roles = "ManagementSupervisor,SuperAdmin")]
    public class IndexModel : BaseModel
    {
        private readonly IAdminDashboardUseCase _useCase;

        [BindProperty]
        public AdminDashboardStatsDto Stats { get; private set; } = new();

        public IndexModel(IAdminDashboardUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task OnGetAsync()
        {
            var managementId = int.Parse(
                User.FindFirst("management_id")!.Value);

            Stats = await _useCase.ExecuteAsync(ManagementId.Value,isSuperAdmin);
        }
    }
}
