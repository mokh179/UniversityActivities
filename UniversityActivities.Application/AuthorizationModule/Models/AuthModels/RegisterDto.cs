using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityActivities.Application.AuthorizationModule.Models.AuthModels
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [RegularExpression(
       @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
       ErrorMessage = "Email contains invalid characters")]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;


       // [Required(ErrorMessage = "Password is required")]
       // [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
       // [RegularExpression(
       //@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&^_])[A-Za-z\d@$!%*#?&^_]{8,}$",
       //ErrorMessage = "Password must contain upper, lower, number, and special character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Number is required")]
        [RegularExpression(
      @"^26\d{8}$",
      ErrorMessage = "Value must start with 26 and be exactly 10 digits" )]
        public string NationalId { get; set; } = string.Empty;


        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Gender is required")]
        [Range(1, 2, ErrorMessage = "Gender must be either 1 or 2")]
        public Gender Gender { get; set; }
        public int ManagmentId { get; set; }
        
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

}
