using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMA.Security.Models;
using MMA.Security.Persistence;
using Shared;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MMA.Security.Migrations.Seed;


public class SecuriyDbSeedingService : IHostedService {
    private readonly IServiceProvider _serviceProvider;


    public SecuriyDbSeedingService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SecurityDbContext>();

        //context.Database.Migrate();

        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

        await SeedInitialData(context, userManager, roleManager, cancellationToken);
    }

    private async Task SeedInitialData(SecurityDbContext context
        , UserManager<ApplicationUser> userManager
        , RoleManager<ApplicationRole> roleManager
        , CancellationToken cancellationToken) {
        await SeedDefaultUserAsync(userManager, roleManager);
        //await SeedSampleDataAsync(context);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    #region Internal functions
    public async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) {
        var roles = new List<ApplicationRole>()
        {
            new ApplicationRole(1, "SysAdmin", "System Admin"),
            new ApplicationRole(2, "Admin", "Admin"),
            new ApplicationRole(3, "User", "General user")
        };

        if (!roleManager.Roles.Any()) {
            foreach (var role in roles) {
                await roleManager.CreateAsync(role);
            }

        }

        var administrator = new ApplicationUser { Id = 1, UserName = "admin", Email = "admin@localhost" };

        var claims = new List<Claim> {
            new Claim("Department", "IT"),
            new Claim("Designation", "Software Engineer")
        };
        if (userManager.Users.All(u => u.UserName != administrator.UserName)) {
            var res = await userManager.CreateAsync(administrator, "Qwe@1234");
            await userManager.AddToRolesAsync(administrator, roles.Select(s => s.Name).ToList());
            await userManager.AddClaimsAsync(administrator, claims);
        }
    }

    public async Task SeedSampleDataAsync(SecurityDbContext context) {
        // Seed, if necessary
        //if (!context.Indications.Any())
        //{
        //    string[] Summaries = new[]
        //    {
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //    };
        //    string[] locations = new[]
        //    {
        //    "Dhaka", "Faridpur", "Rajbari", "Jashor", "Khulna", "Potuakhali", "Munsigang"
        //    };
        //    var data = Enumerable.Range(1, 100).Select(index => new WeatherForecastEntity
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(25, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        //        Location = locations[Random.Shared.Next(locations.Length)]
        //    })
        //    .ToArray();
        //    await context.Indications.AddRangeAsync(data);

        //    await context.SaveChangesAsync();
        //}
    }
    #endregion
}
