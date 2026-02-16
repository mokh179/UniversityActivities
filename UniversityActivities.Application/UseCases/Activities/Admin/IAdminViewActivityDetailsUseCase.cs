using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;

namespace UniversityActivities.Application.UserCases.Activities.Admin
{
    public interface IAdminViewActivityDetailsUseCase
    {
        Task<ActivityAdminDetailsDto?> ExecuteAsync(
      int activityId);
    }
}
