using MeraStore.Shared.Common.Logging.Models;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Serilog;
using System.Text;

namespace MeraStore.Shared.Common.WebApi.Middlewares;

public class RequestResponseLoggingMiddleware(RequestDelegate next)
{
  public async Task Invoke(HttpContext context)
  {
    var payload = new Payload
    {
      Method = context.Request.Method,
      Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
      Path = context.Request.Path,
      RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
      // Capture the request body
      Request = await CaptureRequestBody(context)
    };

    // Capture the original response stream
    var originalBodyStream = context.Response.Body;

    using (var responseBody = new MemoryStream())
    {
      context.Response.Body = responseBody;

      // Call the next middleware in the pipeline
      await next(context);

      // Capture the response details
      payload.StatusCode = context.Response.StatusCode;
      payload.ResponseHeaders = context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
      payload.Response = await CaptureResponseBody(context);

      // Push each field in the Payload model to LogContext
      using (LogContext.PushProperty("Method", payload.Method))
      using (LogContext.PushProperty("Url", payload.Url))
      using (LogContext.PushProperty("Path", payload.Path))
      using (LogContext.PushProperty("RequestHeaders", payload.RequestHeaders))
      using (LogContext.PushProperty("request", payload.Request))
      using (LogContext.PushProperty("ResponseHeaders", payload.ResponseHeaders))
      using (LogContext.PushProperty("response", payload.Response))
      using (LogContext.PushProperty("StatusCode", payload.StatusCode))
      {
        // Log the entire payload
        Log.Information("HTTP Request and Response: {@Payload}", payload);
      }

      // Copy the response body back to the original stream
      await responseBody.CopyToAsync(originalBodyStream);
    }
  }

  private async Task<string> CaptureRequestBody(HttpContext context)
  {
    context.Request.EnableBuffering();

    using (var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
    {
      var body = await reader.ReadToEndAsync();
      context.Request.Body.Position = 0;
      return body;
    }
  }

  private async Task<string> CaptureResponseBody(HttpContext context)
  {
    context.Response.Body.Seek(0, SeekOrigin.Begin);
    var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
    context.Response.Body.Seek(0, SeekOrigin.Begin);
    return text;
  }
}