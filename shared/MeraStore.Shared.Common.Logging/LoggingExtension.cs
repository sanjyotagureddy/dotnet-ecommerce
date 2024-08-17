﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;


namespace MeraStore.Shared.Common.Logging;

public static class LoggingExtension
{
  public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
  {
    var settings = new ConnectionSettings(new Uri(configuration["ElasticConfiguration:Uri"]!))
      .DefaultIndex("request-response-logs");

    var elasticClient = new ElasticClient(settings);
    services.AddSingleton<IElasticClient>(elasticClient);

    return services;
  }
  public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string appName)
  {
    return hostBuilder.UseSerilog((context, configuration) =>
    {
      var elasticUri = context.Configuration["ElasticConfiguration:Uri"];
      // Base configuration for all logs
      configuration
          .ReadFrom.Configuration(context.Configuration)
          .Enrich.WithProperty("app-name", appName)
          .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName ?? "Development")
          .Enrich.WithAssemblyName()
          .Enrich.WithMachineName()
          .WriteTo.Console(outputTemplate: $"[{{SourceContext}}-{appName}-{{MachineName}}]{{NewLine}}{{Timestamp:ddMMyyyy HH:mm:ss}} {{Level:u4}}] {{Message:lj}}{{NewLine}}{{Exception}}");

      // EF Core logs to a different index
      configuration.WriteTo.Logger(logger => logger
        .Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore"))
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri!))
          {
            IndexFormat = $"efcore-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
          }));

      // ASP.NET Core logs to a different index
      configuration.WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(Matching.FromSource("Microsoft.AspNetCore"))
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri!))
          {
            IndexFormat = $"aspnetcore-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
          }));

      // General application logs to a different index
      configuration.WriteTo
        .Logger(lc => lc
          .Filter.ByExcluding(Matching.FromSource("Microsoft"))
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri!))

          {
            IndexFormat = $"app-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 2,
            NumberOfReplicas = 1
          }));
    });
  }
}
