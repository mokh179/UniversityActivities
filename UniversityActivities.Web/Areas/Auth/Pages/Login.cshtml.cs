using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.UserCases.Student.Auth;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Auth.Pages
{
    public class LoginModel : BaseModel
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
            var result = await _loginUseCase.ExecuteAsync(Input);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
            new Claim(ClaimTypes.Name, result.UserName),
            new Claim(ClaimTypes.Role, "Student")
        };

            var identity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(
                "Cookies",
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Index", new { area = "Student" });
        }
    }
}
