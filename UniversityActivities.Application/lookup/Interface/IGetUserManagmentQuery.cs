using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;

namespace UniversityActivities.Application.lookup.Interface
{
    public interface IGetUserManagmentQuery
    {
        Task<List<ManagementUsersDto>> GetUsersinManagement(int id, string? role = null, string? name = null);
    }
}
