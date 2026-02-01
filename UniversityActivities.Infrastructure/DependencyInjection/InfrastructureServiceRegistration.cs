using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityActivities.Application.ApplyUseCases.AdminActivties;
using UniversityActivities.Application.ApplyUseCases.Evaluations;
using UniversityActivities.Application.ApplyUseCases.StudentActivties;
using UniversityActivities.Application.ApplyUseCases.StudentAuth;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.Interfaces.Repositories.Roles;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Application.UserCases.Student.Auth;
using UniversityActivities.Infrastructure.Identity;
using UniversityActivities.Infrastructure.Identity.Services;
using UniversityActivities.Infrastructure.Persistence;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Evaluation;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Students;
using UniversityActivities.Infrastructure.Persistence.Repositories.Roles;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =========================
        // DbContext
        // =========================
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ));

        // =========================
        // Identity
        // =========================
        services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IIdentityMangment, IdentityMangment>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();

        // =========================
        // Repositories
        // =========================
        services.AddScoped<IActivityAssignmentRepository,
                           ActivityAssignmentRepository>();

        services.AddScoped<IManagementSupervisorRepository,
                           ManagementSupervisorRepository>();

        services.AddScoped<IStudentActivityRepository,
                           StudentActivityRepository>();

        services.AddScoped<IStudentActivityQueryRepository,
                           StudentActivityQueryRepository>();

        services.AddScoped<IStudentActivityEvaluationRepository,
                           StudentActivityEvaluationRepository>();

        // =========================
        // Unit Of Work
        // =========================
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // =========================
        // Use Cases - Management
        // =========================
        services.AddScoped<IAssignManagementSupervisorUseCase,
                           AssignManagementSupervisorUseCase>();

        // =========================
        // Use Cases - Activity (Admin)
        // =========================
        services.AddScoped<ICreateActivityUseCase,
                           CreateActivityUseCase>();

        services.AddScoped<IUpdateActivityUseCase,
                           UpdateActivityUseCase>();

        services.AddScoped<IDeleteActivityUseCase,
                           DeleteActivityUseCase>();

        services.AddScoped<IPublishActivityUseCase,
                           PublishActivityUseCase>();

        //services.AddScoped<IViewActivityEvaluationUseCase,ViewActivityEvaluationUseCase>();

        // =========================
        // Use Cases - Activity (Student)
        // =========================
        services.AddScoped<IRegisterStudentInActivityUseCase,
                           RegisterStudentInActivityUseCase>();

        services.AddScoped<IMarkStudentAttendanceUseCase,
                           MarkStudentAttendanceUseCase>();

        services.AddScoped<IViewStudentActivitiesUseCase,ViewStudentUseCase>();
        services.AddScoped<IStudentSignUpUseCase, StudentSignUpUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        // =========================
        // Use Cases - Evaluation
        // =========================
        services.AddScoped<ISubmitActivityEvaluationUseCase,
                           SubmitActivityEvaluationUseCase>();


        return services;
    }
}
