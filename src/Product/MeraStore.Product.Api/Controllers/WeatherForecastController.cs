using Microsoft.AspNetCore.Mvc;

namespace MeraStore.Product.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  private readonly ILogger<WeatherForecastController> _logger = logger;

  [HttpGet(Name = "GetWeatherForecast")]
  public IActionResult Get()
  {
    return Ok(new Dictionary<string, string>()
    {
      {"key1", "value1" },
      { "key2", "value2" }
    });
  }
}
