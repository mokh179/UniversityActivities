using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace UniversityActivities.Web.Common
{
    public abstract class BaseModel:PageModel
    {
        public int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public int? ManagementId =>
            int.TryParse(User.FindFirst("management_id")?.Value, out var id)
                ? id : null;

        public string? Gender =>
            User.FindFirst("gender")?.Value;
    }
}
