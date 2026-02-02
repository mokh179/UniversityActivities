using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.UserCases.Student.Auth;

namespace UniversityActivities.Web.Areas.Auth.Pages
{
    public class LogoutModel : PageModel
    {
        ILogOutUseCase _logOutUseCase;
        public LogoutModel(ILogOutUseCase logOutUseCase)
        {
                _logOutUseCase = logOutUseCase; 
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _logOutUseCase.ExecuteAsync();
            return RedirectToPage("/Login", new { area = "Auth" });
        }
    }
}
