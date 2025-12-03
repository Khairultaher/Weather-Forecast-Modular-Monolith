using Microsoft.AspNetCore.Mvc;
using MMA.Weather.Models;
using Shared;

namespace MMA.Weather.Features.WeatherForcast.Get;

public partial class WeatherForcastController : BaseController {

    private GetWeatherForcastHandler _handler;
    public WeatherForcastController(GetWeatherForcastHandler handler) {
        _handler = handler;
    }

    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastEntity>>> Get([FromBody] GetWeatherForcastQuery query) {
        var forecasts = await _handler.Handle(query);
        return forecasts;
    }
}
