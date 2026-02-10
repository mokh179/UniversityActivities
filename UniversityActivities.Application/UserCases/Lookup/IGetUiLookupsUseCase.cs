using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;

namespace UniversityActivities.Application.UserCases.Lookup
{
    public interface IGetUiLookupsUseCase
    {
        Task<UiLookupsDto> ExecuteAsync();
    }
}
