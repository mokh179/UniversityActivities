using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Admin.Pages.Activities
{
    public class ActivityParticipationDetailsModel : BaseModel
    {
        private readonly IAdminActivityDetailsQuery _adminActivityDetailsQuery;
         public ActivityParticipationDetailsModel(IAdminActivityDetailsQuery adminActivityDetailsQuery)
        {
                _adminActivityDetailsQuery = adminActivityDetailsQuery;
        }
       
        public void OnGet()
        {
        }
    }
}
