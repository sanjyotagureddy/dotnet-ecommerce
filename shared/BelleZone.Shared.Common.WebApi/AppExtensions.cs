using BelleZone.Shared.Common.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BelleZone.Shared.Common.WebApi;

public static class AppExtensions
{
  public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
  {
    app.UseMiddleware<ErrorHandlingMiddleware>();
    return app;
  }
}