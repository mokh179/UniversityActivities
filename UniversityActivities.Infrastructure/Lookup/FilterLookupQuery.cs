using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Domain.Lookups;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Lookup
{
    public class FilterLookupQuery : IfilterLookupQuery
    {
        private readonly AppDbContext _context;

        public FilterLookupQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LookupDto>> GetByTypeAsync(int managementTypeId)
        {
            return await _context.Managements
                .Where(x => x.ManagementTypeId == managementTypeId)
                .Select(x => new LookupDto
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn
                }).ToListAsync();

        }
        public async Task<List<LookupDto>> GetByClubAsync(int managementId)
        {
            return await _context.Clubs
                .Where(x => x.ManagementId == managementId)
                .Select(x => new LookupDto
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn
                }).ToListAsync();
        }

       
    }
}
