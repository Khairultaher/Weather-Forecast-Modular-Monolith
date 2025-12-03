using System;
using System.Collections.Generic;
using System.Text;

namespace MMA.Weather.Features.WeatherForcast.Get;

public class GetWeatherForcastQuery {
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
}
