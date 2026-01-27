using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;

namespace UniversityActivities.Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        IAdminActivityRepository AdminActivities { get; }
        IActivityTargetAudienceRepository ActivityTargetAudiences { get; }
        IActivityAssignmentRepository ActivityAssignments { get; }
        Task<int> SaveChangesAsync();
    }
}
