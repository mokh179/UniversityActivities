using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.Interfaces.Repositories.Clubs;
using UniversityActivities.Application.Interfaces.Repositories.Roles;
using UniversityActivities.Infrastructure.Identity;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Evaluation;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Students;
using UniversityActivities.Infrastructure.Persistence.Repositories.Clubs;
using UniversityActivities.Infrastructure.Persistence.Repositories.Roles;

namespace UniversityActivities.Infrastructure.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public UnitOfWork(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // =========================
        // Admin Activity
        // =========================
        public IAdminActivityRepository AdminActivities
            => new AdminActivityRepository(_context);
        public IActivityTargetAudienceRepository ActivityTargetAudiences
            => new ActivityTargetAudienceRepository(_context);

        public IActivityAssignmentRepository ActivityAssignments
            => new ActivityAssignmentRepository(_context, _roleManager, _userManager);

        //// =========================
        //// Student Activity
        //// =========================
        public IStudentActivityRepository StudentActivities
            => new StudentActivityRepository(_context);

        public IStudentActivityQueryRepository StudentActivityQueries
            => new StudentActivityQueryRepository(_context);

        //// =========================
        //// Evaluation
        //// =========================
        public IStudentActivityEvaluationRepository StudentActivityEvaluations
            => new StudentActivityEvaluationRepository(_context);

        public IAdminActivityEvaluationQueryRepository AdminActivityEvaluations
            => new AdminActivityEvaluationQueryRepository(_context);
        //// =========================
        //// Role
        //// =========================
        public IManagementSupervisorRepository ManagementSupervisors
            => new ManagementSupervisorRepository(_context);
        //// =========================
        //// Clubs
        //// =========================
        public IClubRepository Clubs
            => new ClubRepository(_context);





        // =========================
        // Save
        // =========================
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
