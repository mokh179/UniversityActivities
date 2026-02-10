using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UniversityActivities.Application.lookup.Dto;

namespace UniversityActivities.Application.lookup.Interface
{
    public interface IUiLookupsQuery
    {
        Task<UiLookupsDto> GetAllAsync();
    }
}
