using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.Interfaces.Repositories.Activies.Scan;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.Interfaces.Repositories.Clubs;
using UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers;
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
        IActivityScanRepository ActivityScanRepository { get; }

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
        IClubRepository Clubs {  get; }
        IClubUserRepository ClubUsers {  get; }

        // =========================
        // Persistence
        // =========================
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
