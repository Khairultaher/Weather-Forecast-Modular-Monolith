using MMA.Weather.Models;

namespace MMA.Weather.Features.WeatherForcast.Create;

public class CreateWeatherForcastHandler {

    // inject db context, logger and others
    public CreateWeatherForcastHandler() {

    }

    public async Task<WeatherForecastEntity> Handle(CreateWeatherForcastCommand request) {

        var forecast = new WeatherForecastEntity {
            Date = request.Date,
            TemperatureC = request.TemperatureC,
            Summary = request.Summary
        };

        return forecast;
    }
}
