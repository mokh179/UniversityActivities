using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Lookup
{
    public class UiLookupsQuery : IUiLookupsQuery
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "UI_LOOKUPS";

        public UiLookupsQuery(AppDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<UiLookupsDto> GetAllAsync()
        {
            if (_cache.TryGetValue(CacheKey, out UiLookupsDto cached))
                return cached;

            var data = new UiLookupsDto
            {
                Managements = await _context.Managements
                    .Select(x => new LookupDto
                    {
                        Id = x.Id,
                        NameAr = x.NameAr,
                        NameEn = x.NameEn
                    }).ToListAsync(),
                ActivityTypes = await _context.ActivityTypes
                .Select(x => new LookupDto
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn
                }).ToListAsync(),

                AttendanceScopes = await _context.AttendanceScopes
                .Select(x => new LookupDto
                {
                    Id = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn
                }).ToListAsync()
            };

            _cache.Set(CacheKey, data, TimeSpan.FromHours(6));
            return data;
        }
    }
}
