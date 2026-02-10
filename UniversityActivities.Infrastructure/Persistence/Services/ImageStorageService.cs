using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UniversityActivities.Application.Exceptions;
using UniversityActivities.Application.Interfaces.ImageStorage;

namespace UniversityActivities.Infrastructure.Persistence.Services
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string?> SaveOrReplaceActivityImageAsync(
        IFormFile image,
        Guid activityId,
        string activityTitle,
        string? oldImageUrl)
        {


            try
            {
                ImageValidation.ValidateImage(image);

                // Root: wwwroot
                var rootPath = _env.WebRootPath;

                // images
                var imagesPath = Path.Combine(rootPath, "images");
                if (!Directory.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);

                // images/activities
                var activitiesPath = Path.Combine(imagesPath, "activities");
                if (!Directory.Exists(activitiesPath))
                    Directory.CreateDirectory(activitiesPath);

                // images/activities/{activityId}
                var activityFolder = Path.Combine(activitiesPath, activityId.ToString());
                if (!Directory.Exists(activityFolder))
                    Directory.CreateDirectory(activityFolder);

                // filename from title
                var safeTitle = FileNameHelper.ToSafeFileName(activityTitle);
                var extension = Path.GetExtension(image.FileName);
                var fileName = $"{safeTitle}{extension}";

                // full path
                var fullPath = Path.Combine(activityFolder, fileName);

                // save image
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // return public url
                return $"/images/activities/{activityId}/{fileName}";
            }
            catch (Exception)
            {

                throw new BusinessException("Error occured while saving ima");
            }
          
        }
    }


    public static class FileNameHelper
    {
        public static string ToSafeFileName(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "activity";

            var safe = title.Trim().ToLower();

            // replace spaces with -
            safe = Regex.Replace(safe, @"\s+", "-");

            // remove invalid chars
            safe = Regex.Replace(safe, @"[^a-z0-9\-]", "");

            return safe;
        }
    }

    public static class ImageValidation
    {
        private static readonly string[] AllowedExtensions =
        {
        ".png", ".jpg", ".jpeg"
    };

        public static void ValidateImage(IFormFile image)
        {
            var ext = Path.GetExtension(image.FileName).ToLower();

            if (!AllowedExtensions.Contains(ext))
                throw new Exception("Only PNG, JPG, JPEG images are allowed");

            if (image.Length > 10 * 1024 * 1024)
                throw new Exception("Image size must be less than 10MB");
        }
    }
}
