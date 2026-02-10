using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.Interfaces.Repositories.Roles;

namespace UniversityActivities.Application.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        // =========================
        // Admin Activity
        // =========================
        IAdminActivityRepository AdminActivities { get; }
        IActivityTargetAudienceRepository ActivityTargetAudiences { get; }
        IActivityAssignmentRepository ActivityAssignments { get; }

        // =========================
        // Student Activity
        // =========================
        IStudentActivityRepository StudentActivities { get; }
        IStudentActivityQueryRepository StudentActivityQueries { get; }

        // =========================
        // Evaluation
        // =========================
        IStudentActivityEvaluationRepository StudentActivityEvaluations { get; }
        IAdminActivityEvaluationQueryRepository AdminActivityEvaluations { get; }

        // =========================
        // Roles
        // =========================
        IManagementSupervisorRepository ManagementSupervisors {  get; }

        // =========================
        // Persistence
        // =========================
        Task<int> SaveChangesAsync();
    }
}
