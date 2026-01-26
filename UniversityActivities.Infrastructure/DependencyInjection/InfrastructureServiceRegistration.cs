using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //services.AddDbContext<AppDbContext>(...);

        //services.AddIdentity<ApplicationUser, IdentityRole<int>>(...)
        //        .AddEntityFrameworkStores<AppDbContext>();

        return services;
    }
}
