using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Weather.Features.WeatherForcast.Create;

public class CreateWeatherForcastCommand {
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}
