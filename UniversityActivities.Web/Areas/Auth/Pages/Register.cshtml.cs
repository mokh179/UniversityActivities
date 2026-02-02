using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.UserCases.Student.Auth;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Auth.Pages
{
    public class RegisterModel : BaseModel
    {
        private readonly IStudentSignUpUseCase _signUpUseCase;
        private readonly IUiLookupsQuery _lookupsQuery;

        public RegisterModel(
            IStudentSignUpUseCase signUpUseCase,
            IUiLookupsQuery lookupsQuery)
        {
            _signUpUseCase = signUpUseCase;
            _lookupsQuery = lookupsQuery;
        }

        [BindProperty]
        public RegisterDto Input { get; set; } = new();

        public List<LookupDto> Managements { get; set; } = new();
        public async Task OnGetAsync()
        {
            var lookups = await _lookupsQuery.GetAllAsync();
            Managements = lookups.Managements;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _signUpUseCase.ExecuteAsync(Input);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
            new Claim(ClaimTypes.Name, result.UserName),
            new Claim(ClaimTypes.Role, "Student"),
            new Claim("management_id", Input.ManagmentId.ToString()),
            new Claim("gender", Input.Gender.ToString())
        };

            var identity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(
                "Cookies",
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Index", new { area = "Student" });
        }
    }
}
