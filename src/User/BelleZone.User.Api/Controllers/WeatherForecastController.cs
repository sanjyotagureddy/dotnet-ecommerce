using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace BelleZone.User.Api.Controllers;

[ApiController]
[Route("[controller]")]
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
    ProductValidator validator = new ProductValidator();

    validator.ValidateAndThrow(product);
    return Ok(product);
  }
}
