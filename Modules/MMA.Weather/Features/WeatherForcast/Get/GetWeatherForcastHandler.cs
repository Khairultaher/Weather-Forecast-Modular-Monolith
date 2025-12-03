using MMA.Weather.Models;

namespace MMA.Weather.Features.WeatherForcast.Get;

public class GetWeatherForcastHandler {

    // inject db context, logger and others
    public GetWeatherForcastHandler() {

    }

    public async Task<List<WeatherForecastEntity>> Handle(GetWeatherForcastQuery request) {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecastEntity {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        })
        .ToAsyncEnumerable();

        return await forecast.ToListAsync();
    }
}
