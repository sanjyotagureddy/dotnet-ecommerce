using System.Text.Json;
using System.Text.Json.Serialization;

using MeraStore.Shared.Common.Logging;
using MeraStore.Shared.Common.WebApi.Filters;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeraStore.Shared.Common.WebApi;


public static class ServiceExtensions
{
  public static IServiceCollection AddDefaultServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddControllers().AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
      options.JsonSerializerOptions.WriteIndented = true;
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(sg =>
    {
      sg.OperationFilter<CustomHeadersFilter>();
    });
    services.AddElasticsearch(configuration);
    services.AddHttpContextAccessor();
    services.AddResponseCompression();

    return services;
  }
}