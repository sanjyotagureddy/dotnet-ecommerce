using FluentValidation;
using MeraStore.Shared.Common.Logging.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace MeraStore.User.Api.Controllers;

[ApiController]
[Route("[controller]")]
[EventCode("1xx")]

public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  private readonly ILogger<WeatherForecastController> _logger = logger;

  
  [HttpPost(Name = "GetWeatherForecast")]
  public IActionResult Post([FromBody] Product product)
  {
    var validator = new ProductValidator();

    validator.ValidateAndThrow(product);
    return Ok(product);
  }
}
