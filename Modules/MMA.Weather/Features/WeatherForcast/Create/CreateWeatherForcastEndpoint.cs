using Microsoft.AspNetCore.Mvc;
using MMA.Weather.Models;
using Shared;

namespace MMA.Weather.Features.WeatherForcast.Create;

public partial class WeatherForcastController : BaseController {

    private CreateWeatherForcastHandler _handler;
    public WeatherForcastController(CreateWeatherForcastHandler handler) {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<WeatherForecastEntity>> Create([FromBody] CreateWeatherForcastCommand command) {
        var forecast = await _handler.Handle(command);
        return forecast;
    }
}
