using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.Exceptions;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.UserCases.Student.Auth;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Auth.Pages
{
    public class RegisterModel : PageModel
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
            if (!ModelState.IsValid)
            {
               await OnGetAsync(); // important to reload dropdown
                return Page();
            }
            try

            {
                    var result = await _signUpUseCase.ExecuteAsync(Input);
                    return RedirectToPage("/Index", new { area = "Student" });
            }
	        catch (BusinessException ex)

            {

                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync();
                return Page();
            }
        }
    }
}
