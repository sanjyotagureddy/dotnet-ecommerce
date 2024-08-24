using FluentValidation;
using MeraStore.Shared.Common.Http;
using MeraStore.Shared.Common.Http.Extensions;
using MeraStore.Shared.Common.Logging.Attributes;
using MeraStore.User.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace MeraStore.User.Api.Controllers;

[ApiController]
[Route("[controller]")]
[EventCode("1xx")]

public class WeatherForecastController(ILogger<WeatherForecastController> logger, IApiClient apiClient) : ControllerBase
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

  [HttpGet]
  public async Task<IActionResult> GetAsync()
  {
    var request = new HttpRequestBuilder()
      .WithUri("https://localhost:7051/WeatherForecast")
      .WithMethod(HttpMethod.Get)
      .WithHeader(Constants.Headers.CorrelationId, Guid.NewGuid().ToString());

    var response = await apiClient.ExecuteAsync(request);

    var responseContent = await response.GetResponseOrError<Dictionary<string, string>>();

    return Ok(responseContent.Result);


  }
}
