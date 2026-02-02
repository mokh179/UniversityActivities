using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Web.Common;

namespace UniversityActivities.Web.Areas.Student.Pages
{
    [Authorize]
    public class IndexModel : BaseModel
    {
        private readonly IViewStudentActivitiesUseCase _viewStudentActivitiesUseCase;
        public IndexModel(IViewStudentActivitiesUseCase viewStudentActivitiesUseCase)
        {
              _viewStudentActivitiesUseCase = viewStudentActivitiesUseCase;
        }
        public int JoinedActivitiesCount { get; set; }
        public int UpcomingActivitiesCount { get; set; }
    
        [BindProperty(SupportsGet = true)]
        public string? SelectedCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedStatus { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? DateRange { get; set; }
        // List
        public List<ActivityDto> Activities { get; set; } = new();
        public List<(string Type, string Value)> Claims { get; set; } = new();
        public void OnGet(string? category, string? status, string? remove)
        {
            // Demo values (replace with Application Layer)
            JoinedActivitiesCount = 5;
            UpcomingActivitiesCount = 2;
            var allActivities = new List<ActivityDto>
        {
            new() { Id = 1, Title = "Football Tournament", Category = "Sports", Status = "Upcoming", Date = DateTime.Today },
            new() { Id = 2, Title = "Tech Talk AI", Category = "Tech", Status = "Upcoming", Date = DateTime.Today.AddDays(2) },
            new() { Id = 3, Title = "Cultural Day", Category = "Culture", Status = "Finished", Date = DateTime.Today.AddDays(-3) }
        };
            if (remove == "Category") category = null;
            if (remove == "Status") status = null;

            SelectedCategory = category;
            SelectedStatus = status;
            Activities = allActivities;
        }
    }

    public class ActivityDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; } = "/images/activity-placeholder.jpg";
    }


}
