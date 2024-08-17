using System.Text.Json;
using System.Text.Json.Serialization;
using MeraStore.Shared.Common.Logging.Exceptions;
using MeraStore.Shared.Common.Logging.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeraStore.Shared.Common.WebApi.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore // Ignore null values
    };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (FluentValidation.ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (BaseAppException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            var wrappedException = new BaseAppException("GEN", GetRequestEventCode(context), "500", $"An unexpected error occurred. Exception message: {ex.Message}. | Exception stack: {ex?.InnerException?.ToString()}");
            await HandleExceptionAsync(context, wrappedException);
        }
    }

    private Task HandleValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = 400; // Bad Request for validation errors

        var validationErrors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        var problemDetails = new ValidationProblemDetails(validationErrors)
        {
            Type = GetRequestEventCode(context),
            Status = context.Response.StatusCode,
            Title = "One or more validation errors occurred.",
            Instance = context.TraceIdentifier
        };
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, _jsonSerializerSettings));
    }

    private Task HandleExceptionAsync(HttpContext context, BaseAppException exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = 500; // Default to 500 for unexpected errors

        var problemDetails = new ProblemDetails()
        {
            Status = context.Response.StatusCode,
            Type = GetRequestEventCode(context),
            Title = "An error occurred while processing your request.",
            Detail = exception.Message,
            Extensions =
            {
                ["errorCode"] = exception.FullErrorCode,
                ["service"] = exception.ServiceIdentifier
            }
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, _jsonSerializerSettings));
    }

    /// <summary>
    /// This gets the request event code associated with any endpoint of any service
    /// for quicker dereferencing and diagnosis.
    /// All endpoints to be created are to be decorated with `EventCodeAttribute`
    /// with a unique value.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private string GetRequestEventCode(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        string code = endpoint?.Metadata.GetMetadata<EventCodeAttribute>()?.EventCode ?? string.Empty;
        return code;
    }
}