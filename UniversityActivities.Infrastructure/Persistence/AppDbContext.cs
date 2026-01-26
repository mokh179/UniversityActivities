using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniversityActivities.Domain.Lookups;
using UniversityActivities.Infrastructure.Identity;

namespace UniversityActivities.Infrastructure.Persistence
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        // =========================
        // Management
        // =========================
        public DbSet<Management> Managements { get; set; }
        public DbSet<ManagementType> ManagementTypes { get; set; }

        // =========================
        // Clubs
        // =========================
        public DbSet<StudentClub> StudentClubs { get; set; }
        public DbSet<ClubMember> ClubMembers { get; set; }

        // =========================
        // Activities
        // =========================
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityUser> ActivityUsers { get; set; }

        // =========================
        // Student Participation
        // =========================
        public DbSet<StudentActivity> StudentActivities { get; set; }
        public DbSet<ActivityEvaluation> ActivityEvaluations { get; set; }
        public DbSet<EvaluationScore> EvaluationScores { get; set; }
        public DbSet<EvaluationCriteria> EvaluationCriteria { get; set; }

        // =========================
        // Lookups
        // =========================
        public DbSet<ActivityStatus> ActivityStatuses { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<TargetAudience> TargetAudiences { get; set; }
        public DbSet<AttendanceMode> AttendanceModes { get; set; }
        public DbSet<AttendanceScope> AttendanceScopes { get; set; }
        public DbSet<ClubDomain> ClubDomains { get; set; }

        // =========================
        // RBAC - Definitions
        // =========================
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ManagementRole> ManagementRoles { get; set; }
        public DbSet<ClubRole> ClubRoles { get; set; }
        public DbSet<ActivityRole> ActivityRoles { get; set; }

        // =========================
        // RBAC - Links
        // =========================
        public DbSet<ManagementRolePermission> ManagementRolePermissions { get; set; }
        public DbSet<ClubRolePermission> ClubRolePermissions { get; set; }
        public DbSet<ActivityRolePermission> ActivityRolePermissions { get; set; }

        // =========================
        // Runtime User Roles
        // =========================
        public DbSet<UserManagementRole> UserManagementRoles { get; set; }


        // =====================================================
        // Model Creating
        // =====================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplySoftDeleteFilter(modelBuilder);
            ApplyUniqueConstraints(modelBuilder);
        }

        private static void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType)
                                .HasQueryFilter(lambda);
                }
            }
        }



        private static void ApplyUniqueConstraints(ModelBuilder modelBuilder)
        {
            // Student registers once per activity
            modelBuilder.Entity<StudentActivity>()
                .HasIndex(x => new { x.StudentId, x.ActivityId })
                .IsUnique();

            // One evaluation per student per activity
            modelBuilder.Entity<ActivityEvaluation>()
                .HasIndex(x => new { x.StudentId, x.ActivityId })
                .IsUnique();

            // One membership per user per club
            modelBuilder.Entity<ClubMember>()
                .HasIndex(x => new { x.UserId, x.StudentClubId })
                .IsUnique();
        }
    }
}
