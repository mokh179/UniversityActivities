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
        public async Task<List<ManagementUsersDto>> GetUsersinManagement(int id,string? role=null, string? name=null)
        {
            var result = new List<ManagementUsersDto>();

            if (!string.IsNullOrWhiteSpace(role))
            {
                var queryRole = await _userManager.GetUsersInRoleAsync(role);
                var users=queryRole.Where(u => u.ManagementId == id);
                foreach (var user in users)
                {
                    result.Add(new ManagementUsersDto
                    {
                        Id = user.Id,
                        Name = user.FirstName! + '\t' + user.MiddleName! + '\t' + user.LastName!,
                        username = user.UserName!,
                        Isnew = false
                    });
                }
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                var query = _userManager.Users.Where(u => u.ManagementId == id);
                var users = query.Where(u =>
                    u.UserName!.Contains(name) ||
                    u.Email!.Contains(name));
                foreach (var user in users)
                {
                    result.Add(new ManagementUsersDto
                    {
                        Id = user.Id,
                        Name = user.FirstName! + '\t' + user.MiddleName! + '\t' + user.LastName!,
                        username = user.UserName!,
                        Isnew = false
                    });
                }
            }
            return result;
        }
    }
}
