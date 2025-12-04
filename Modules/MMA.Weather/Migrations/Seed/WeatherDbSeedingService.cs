using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMA.Weather.Models;
using MMA.Weather.Persistence;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MMA.Weather.Migrations.Seed;


public class WeatherDbSeedingService : IHostedService {
    private readonly IServiceProvider _serviceProvider;


    public WeatherDbSeedingService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WeatherForecastDbContext>();

        //context.Database.Migrate();

        await SeedInitialData(context, cancellationToken);
    }

    private async Task SeedInitialData(WeatherForecastDbContext context
        , CancellationToken cancellationToken) {
        await SeedSampleDataAsync(context);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    #region Internal functions
  
    public async Task SeedSampleDataAsync(WeatherForecastDbContext context) {
        // Seed, if necessary
        if (!context.WeatherForecasts.Any()) {
            string[] Summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            string[] locations = new[]
            {
            "Dhaka", "Faridpur", "Rajbari", "Jashor", "Khulna", "Potuakhali", "Munsigang"
            };
            var data = Enumerable.Range(1, 100).Select(index => new WeatherForecastEntity {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TemperatureC = Random.Shared.Next(25, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            await context.WeatherForecasts.AddRangeAsync(data);

            await context.SaveChangesAsync();
        }
    }
    #endregion
}
