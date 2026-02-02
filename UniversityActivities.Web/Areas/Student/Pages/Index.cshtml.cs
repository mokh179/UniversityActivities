using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    [Authorize]
    public class IndexModel : BaseModel
    {
        public int JoinedActivitiesCount { get; set; }
        public int UpcomingActivitiesCount { get; set; }
        public List<(string Type, string Value)> Claims { get; set; } = new();
        public void OnGet()
        {
            // Demo values (replace with Application Layer)
            JoinedActivitiesCount = 5;
            UpcomingActivitiesCount = 2;
            // كلهم جايين من Claims
            Claims = User.Claims
                .Select(c => (c.Type, c.Value))
                .ToList();
        }
    
    }
}
