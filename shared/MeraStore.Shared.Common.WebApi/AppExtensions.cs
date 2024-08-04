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
}