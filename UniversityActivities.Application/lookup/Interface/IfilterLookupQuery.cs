using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;

namespace UniversityActivities.Application.lookup.Interface
{
    public interface IfilterLookupQuery
    {
        Task<List<LookupDto>> GetByTypeAsync(int managementTypeId);
        Task<List<LookupDto>> GetByClubAsync(int managementId);
    }
}
