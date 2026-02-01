using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using UniversityActivities.Domain.Common;
using UniversityActivities.Domain.Entities;
using UniversityActivities.Domain.Lookups;
using UniversityActivities.Infrastructure.Identity;

namespace UniversityActivities.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
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
        public DbSet<ActivityTargetAudience> ActivityTargetAudiences { get; set; }

        // =========================
        // Student Participation
        // =========================
        public DbSet<StudentActivity> StudentActivities { get; set; }
        public DbSet<ActivityEvaluation> ActivityEvaluations { get; set; }
        public DbSet<ActivityEvaluationComment> ActivityEvaluationComments { get; set; }
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
        // Runtime User Roles
        // =========================
        public DbSet<ManagementSupervisors> ManagementSupervisors { get; set; }

        // =====================================================
        // Model Creating
        // =====================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply IEntityTypeConfiguration<> implementations you place in the Infrastructure assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ApplySoftDeleteFilter(modelBuilder);
            ApplyUniqueConstraints(modelBuilder);

            // Avoid accidental cascade deletes when using soft-delete
            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
        {
            var baseEntityType = typeof(BaseEntity);
            var efPropertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Public | BindingFlags.Static)!;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (!baseEntityType.IsAssignableFrom(clrType) || clrType.IsAbstract)
                    continue;

                // Build: e => EF.Property<bool>(e, "IsDeleted") == false
                var parameter = Expression.Parameter(clrType, "e");
                var efPropertyGeneric = efPropertyMethod.MakeGenericMethod(typeof(bool));
                var propertyCall = Expression.Call(efPropertyGeneric, parameter, Expression.Constant(nameof(BaseEntity.IsDeleted)));
                var condition = Expression.Equal(propertyCall, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);

                modelBuilder.Entity(clrType).HasQueryFilter(lambda);
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

        // =====================================================
        // Auditing & Soft-delete behavior on Save
        // =====================================================
        public override int SaveChanges()
        {
            ApplyAuditInformation();
            HandleSoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInformation();
            HandleSoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditInformation()
        {
            var now = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                }
            }
        }

        private void HandleSoftDelete()
        {
            var now = DateTime.UtcNow;

            // Convert physical deletes into soft-deletes for BaseEntity types
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                if (entry.Entity.DeletedAt == null)
                    entry.Entity.DeletedAt = now;

                // If also AuditableEntity, set UpdatedAt
                if (entry.Entity is AuditableEntity aud)
                {
                    aud.UpdatedAt = now;
                }
            }

            // Ensure DeletedAt is set when IsDeleted toggled
            foreach (var entry in ChangeTracker.Entries<BaseEntity>().Where(e => e.State == EntityState.Modified))
            {
                if (entry.Entity.IsDeleted && entry.Entity.DeletedAt == null)
                {
                    entry.Entity.DeletedAt = now;
                }
            }
        }
    }
}
