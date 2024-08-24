using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Common.Http;

public static class ApiCallerExtensions
{
  public static IServiceCollection AddApiCallerServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddHttpClient();
    services.AddSingleton<IApiClient, ApiClient>();

    return services;
  }
}