using Serilog;
using UniversityActivities.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});


// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    // Account
    options.Conventions.AddAreaPageRoute("Auth", "/Login", "login");
    options.Conventions.AddAreaPageRoute("Auth", "/Register", "register");
    options.Conventions.AddAreaPageRoute("Auth", "/Logout", "logout");
});

builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();

//Looging 
if (builder.Configuration.GetValue<bool>("SeedData"))
{
    await app.SeedDatabaseAsync();
}



Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
