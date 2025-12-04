using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMA.Weather.Features.WeatherForcast.Create;
using MMA.Weather.Features.WeatherForcast.Get;
using MMA.Weather.Persistence;
using Shared;

namespace MMA.Weather;

public static class WeatherModule {
    public static IServiceCollection AddWeatherModule(
        this IServiceCollection services,
        IConfiguration configuration
        ) {


        // register db context

        // Register any services specific to the Weather module here
        // 1️⃣ Read connection string
        var connectionString = configuration.GetConnectionString("WeatherForecastConnection");

        // 2️⃣ Register the DbContext
        services.AddDbContext<WeatherForecastDbContext>(options => {
            options.UseSqlServer(connectionString, sql => {
                sql.MigrationsAssembly("MMA.Weather"); // important
            });

            // EF Core 10 lazy loading
            options.UseLazyLoadingProxies(true);
        });

        services.AddScoped<IWeatherForecastDbContext>(provider => provider.GetRequiredService<WeatherForecastDbContext>());

        services.AddScoped<CreateWeatherForcastHandler>();
        services.AddScoped<GetWeatherForcastHandler>();
        return services;
    }
}
