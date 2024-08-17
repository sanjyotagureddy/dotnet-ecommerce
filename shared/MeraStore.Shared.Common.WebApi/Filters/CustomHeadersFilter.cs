using MeraStore.User.Shared.Common;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MeraStore.Shared.Common.WebApi.Filters;

public class CustomHeadersFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    operation.Parameters ??= new List<OpenApiParameter>();

    if (!operation.Parameters.Any(p => p.Name == Constants.Headers.CorrelationId && p.In == ParameterLocation.Header))
    {
      operation.Parameters.Add(new OpenApiParameter
      {
        Name = Constants.Headers.CorrelationId,
        In = ParameterLocation.Header,
        Required = false, // Set to true if you want to make it mandatory
        Description = "Correlation ID for tracking requests across services.",
        Schema = new OpenApiSchema
        {
          Type = "string"
        }
      });
    }
  }
}