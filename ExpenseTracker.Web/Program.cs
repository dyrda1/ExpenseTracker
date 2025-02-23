using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddHealthChecks();

//Register Syncfusion license
SyncfusionLicenseProvider.RegisterLicense(
    "Mgo+DSMBMAY9C3t2VVhhQlFac1pJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkNjWn9edHNRRmZYWEM=");

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
    // var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // dbContext.Database.Migrate(); // Runs migration once at startup
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseHealthChecks("/health");

app.MapControllerRoute(
    "default",
    "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();