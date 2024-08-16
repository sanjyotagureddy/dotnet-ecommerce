using MeraStore.Shared.Common.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MeraStore.Shared.Common.WebApi;

public static class AppExtensions
{
  public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
  {
    app.UseMiddleware<ErrorHandlingMiddleware>();
    return app;
  }

  public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
  {
    app.UseMiddleware<CorrelationIdMiddleware>();
    return app;
  }

  public static IApplicationBuilder UseCommonMiddlewares(this IApplicationBuilder app)
  {
    app.UseCorrelationId();
    app.UseCustomExceptionHandler();
    return app;
  }
}