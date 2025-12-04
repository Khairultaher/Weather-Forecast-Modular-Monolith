using Microsoft.AspNetCore.Mvc;
using MMA.Weather.Models;
using Shared;

namespace MMA.Weather.Features.WeatherForcast.Create;

public partial class CreateWeatherForcastController : BaseController {

    private CreateWeatherForcastHandler _handler;
    public CreateWeatherForcastController(CreateWeatherForcastHandler handler) {
        _handler = handler;
    }

    [HttpPost]
    [Route("WeatherForecast")]
    public async Task<ActionResult<WeatherForecastEntity>> Create([FromBody] CreateWeatherForcastCommand command) {
        var forecast = await _handler.Handle(command);
        return forecast;
    }
}
