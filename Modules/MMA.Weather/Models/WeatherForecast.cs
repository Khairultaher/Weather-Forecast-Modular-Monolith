using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Weather.Models;

public class WeatherForecastEntity {
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}
