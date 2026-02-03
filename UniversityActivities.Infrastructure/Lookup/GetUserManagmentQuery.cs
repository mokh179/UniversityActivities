using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Infrastructure.Identity;

namespace UniversityActivities.Infrastructure.Lookup
{
    public class GetUserManagmentQuery : IGetUserManagmentQuery
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetUserManagmentQuery(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<ManagementUsersDto>> GetUsersinManagement(int id, string? name=null)
        {
            var query = _userManager.Users
            .Where(u => u.ManagementId == id);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u =>
                    u.UserName!.Contains(name) ||
                    u.Email!.Contains(name));
            }

            var users = await query.ToListAsync();

            var result = new List<ManagementUsersDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Student") || roles.Contains("SuperAdmin"))
                    continue;

                result.Add(new ManagementUsersDto
                {
                    Id = user.Id,
                    Name = user.FirstName!+'\t'+ user.MiddleName! + '\t'+ user.LockoutEnabled!,
                    username = user.UserName!
                });
            }

            return result;
        }
    }
}
