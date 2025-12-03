using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMA.Weather.Features.WeatherForcast.Create;
using MMA.Weather.Features.WeatherForcast.Get;

namespace MMA.Weather;

public static class WeatherModule {
    public static IServiceCollection AddWeatherModule(
        this IServiceCollection services,
        IConfiguration configuration
        ) {
        // Register any services specific to the Weather module here
        services.AddScoped<CreateWeatherForcastHandler>();
        services.AddScoped<GetWeatherForcastHandler>();
        return services;
    }
}
