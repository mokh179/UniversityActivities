using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace UniversityActivities.Web.Common
{
    [Authorize]
    public abstract class BaseModel:PageModel
    {
        public int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public int? ManagementId =>
            int.TryParse(User.FindFirst("Management_id")?.Value, out var id)
                ? id : null;

        public string? Gender =>
            User.FindFirst("Gender")?.Value;

        public bool isSuperAdmin => User.FindFirstValue(ClaimTypes.Role) == "SuperAdmin";
       public string? Rolename => User.FindFirstValue(ClaimTypes.Role);
        public int? TargetAudienceId => int.TryParse(User.FindFirst("TargetAudience")?.Value, out var id)
                ? id : null;


    }
}
