using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UserCases.BackgroundServices.Interface.Activity
{
    public interface IUpdateActivityStatusUseCase
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
