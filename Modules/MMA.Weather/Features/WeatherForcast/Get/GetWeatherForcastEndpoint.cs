using Microsoft.AspNetCore.Mvc;
using MMA.Weather.Models;
using Shared;

namespace MMA.Weather.Features.WeatherForcast.Get;

public partial class GetWeatherForcastController : BaseController {

    private GetWeatherForcastHandler _handler;
    public GetWeatherForcastController(GetWeatherForcastHandler handler) {
        _handler = handler;
    }

    [HttpGet]
    [Route("WeatherForecast")]
    public async Task<ActionResult<List<WeatherForecastEntity>>> Get([FromBody] GetWeatherForcastQuery query) {
        var forecasts = await _handler.Handle(query);
        return forecasts;
    }
}
