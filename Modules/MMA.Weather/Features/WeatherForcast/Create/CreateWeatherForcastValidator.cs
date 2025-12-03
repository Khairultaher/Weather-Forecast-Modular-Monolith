using FluentValidation;

namespace MMA.Weather.Features.WeatherForcast.Create;

public class CreateWeatherForcastValidator : AbstractValidator<CreateWeatherForcastCommand> {

    public CreateWeatherForcastValidator() {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.TemperatureC).NotEmpty();
    }
}
