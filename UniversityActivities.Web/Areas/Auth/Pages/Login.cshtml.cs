using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.Exceptions;
using UniversityActivities.Application.UserCases.Student.Auth;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Auth.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILoginUseCase _loginUseCase;

        public LoginModel(ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        [BindProperty]
        public LoginDto Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var result = await _loginUseCase.ExecuteAsync(Input);
                if(!result.IsAdmin)
                    return RedirectToPage("/Index", new { area = "Student" });
                return RedirectToPage("/Index", new { area = "Admin" });
            }
            catch (BusinessException ex)

            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

        }
    }
}
