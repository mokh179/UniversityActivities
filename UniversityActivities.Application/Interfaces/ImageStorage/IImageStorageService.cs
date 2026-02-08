using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace UniversityActivities.Application.Interfaces.ImageStorage
{
    public interface IImageStorageService
    {
        Task<string?> SaveOrReplaceActivityImageAsync(
            IFormFile image,
            Guid activityId,
            string activityTitle,
            string? oldImageUrl);
    }
}
