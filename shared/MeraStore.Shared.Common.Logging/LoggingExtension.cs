
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Sinks.Elasticsearch;


namespace MeraStore.Shared.Common.Logging;

public static class LoggingExtension
{
  public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string appName)
  {
    return hostBuilder.UseSerilog((context, configuration) =>
    {
      configuration.Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.WithProperty("Application", appName)
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName ?? "Development")
        .Enrich.WithAssemblyName()
        .Enrich.WithMachineName()
        .WriteTo.Console(outputTemplate: "[{SourceContext}]{NewLine}{Timestamp:ddMMyyyy HH:mm:ss} {Level:u4}] {Message:lj}{NewLine}{Exception}")
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
        {

          IndexFormat =
            $"app-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
          AutoRegisterTemplate = true,
          NumberOfShards = 2,
          NumberOfReplicas = 1
        });

      // EF Core logs to a different index
      configuration.WriteTo.Logger(logger => logger
        .Filter.ByIncludingOnly(logEvent => logEvent.Properties.TryGetValue("SourceContext", out var sourceContext)
                                            && sourceContext.ToString().StartsWith("\"Microsoft.EntityFrameworkCore\""))
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
        {
          IndexFormat = $"efcore-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.Now:yyyy-MM}",
          AutoRegisterTemplate = true,
          NumberOfShards = 2,
          NumberOfReplicas = 1
        }));

    });
  }
}