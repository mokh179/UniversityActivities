using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.Common.Models.Background;
using UniversityActivities.Application.Implementation.ApplyUseCases.AdminActivties;
using UniversityActivities.Application.Implementation.ApplyUseCases.AdminActivties.Scan;
using UniversityActivities.Application.Implementation.ApplyUseCases.AdminDashboard;
using UniversityActivities.Application.Implementation.ApplyUseCases.BackgroundServices.Activity;
using UniversityActivities.Application.Implementation.ApplyUseCases.Evaluations;
using UniversityActivities.Application.Implementation.ApplyUseCases.lookup;
using UniversityActivities.Application.Implementation.ApplyUseCases.StudentActivties;
using UniversityActivities.Application.Implementation.ApplyUseCases.StudentAuth;
using UniversityActivities.Application.Implementation.Services;
using UniversityActivities.Application.Interfaces.ImageStorage;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.QRCode;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.Interfaces.Repositories.Activies.Background;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.Interfaces.Repositories.Admin;
using UniversityActivities.Application.Interfaces.Repositories.Roles;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.Mapping.Activities;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Application.UserCases.Activities.Scan;
using UniversityActivities.Application.UserCases.Activities.Student;
using UniversityActivities.Application.UserCases.Admin;
using UniversityActivities.Application.UserCases.BackgroundServices.Interface.Activity;
using UniversityActivities.Application.UserCases.Lookup;
using UniversityActivities.Application.UserCases.Student.Auth;
using UniversityActivities.Infrastructure.BackgroundServices;
using UniversityActivities.Infrastructure.Identity;
using UniversityActivities.Infrastructure.Identity.Services;
using UniversityActivities.Infrastructure.Lookup;
using UniversityActivities.Infrastructure.Persistence;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.BackgroundService;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Evaluation;
using UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Students;
using UniversityActivities.Infrastructure.Persistence.Repositories.Admin;
using UniversityActivities.Infrastructure.Persistence.Repositories.Roles;
using UniversityActivities.Infrastructure.Persistence.Services;

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


        // =========================
        // JWT
        // =========================
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;

        })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
               };
           });


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        });
        services.AddAuthorization();







        services.AddScoped<IIdentityMangment, IdentityMangment>(); // depends on RoleManager/UserManager
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

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
       services.AddScoped<IUserAccessQueryRepository,UserAccessQueryRepository>();

        services.AddScoped<IAdminActivityRepository, AdminActivityRepository>();
        services.AddScoped<IActivityTargetAudienceRepository, ActivityTargetAudienceRepository>();

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

        services.AddScoped<IViewAdminActivitiesUseCase, ViewAdminActivitiesUseCase>();
        services.AddScoped<IViewActivityEvaluationUseCase,ViewActivityEvaluationUseCase>();

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
        services.AddScoped<IUiLookupsQuery, UiLookupsQuery>();
        services.AddScoped<IGetUiLookupsUseCase, GetUiLookupsUseCase >();
        services.AddScoped<IAdminViewActivityDetailsUseCase, AdminViewActivityDetailsUseCase>();
        

        // =========================
        // Use Cases - Evaluation
        // =========================
        services.AddScoped<ISubmitActivityEvaluationUseCase,
                           SubmitActivityEvaluationUseCase>();

        services.AddScoped<
    IUserClaimsPrincipalFactory<ApplicationUser>,
    AppClaimsPrincipalFactory>();


        services.AddScoped<IAdminDashboardUseCase,
                   AdminDashboardUseCase>();

        services.AddScoped<IDashboardQueryRepository,
                           AdminDashboardQueryRepository>();

        // AutoMapper
        services.AddAutoMapper(typeof(ActivityProfile).Assembly);


        services.AddScoped<ILogOutUseCase, LogOutUseCase>();
        services.AddScoped<IfilterLookupQuery, FilterLookupQuery>();
        services.AddScoped<IGetUserManagmentQuery, GetUserManagmentQuery>();
        services.AddScoped<IImageStorageService, ImageStorageService>();
        services.AddScoped<IGenerateAttendanceCertificateUseCase, GenerateAttendanceCertificateUseCase>();
        services.AddScoped<IAdminActivityDetailsQuery, AdminActivityDetailsQuery>();
        services.AddScoped<IActivityStatusRepository,ActivityStatusRepository>();
        services.AddScoped<IUpdateActivityStatusUseCase, UpdateActivityStatusUseCase>();
        services.Configure<ActivityStatusBackgroundOptions>(configuration.GetSection("ActivityStatusBackground"));
        services.AddHostedService<ActivityStatusBackgroundService>();
        services.AddScoped<IHandleActivityScanUseCase, HandleActivityScanUseCase>();
        services.AddScoped<IQRCodeGeneratorService, QRCodeGeneratorService>();
        return services;
    }
}
