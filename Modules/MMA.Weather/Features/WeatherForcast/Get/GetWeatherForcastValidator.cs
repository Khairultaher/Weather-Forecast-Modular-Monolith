using FluentValidation;

namespace MMA.Weather.Features.WeatherForcast.Get;

public class GetWeatherForcastValidator : AbstractValidator<GetWeatherForcastQuery> {

    public GetWeatherForcastValidator() {
        //RuleFor(x => x.FromDate).NotEmpty();
        //RuleFor(x => x.ToDate).NotEmpty();
    }
}
