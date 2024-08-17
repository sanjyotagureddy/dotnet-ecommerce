using System.Diagnostics;
using System.Text;
using MeraStore.Shared.Common.Logging.Models;
using MeraStore.User.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Nest;
using Serilog;
using Serilog.Context;

namespace MeraStore.Shared.Common.WebApi.Middlewares;

public class RequestResponseLoggingMiddleware(RequestDelegate next, IElasticClient elasticClient, IConfiguration configuration)
{
  public async Task Invoke(HttpContext context)
  {
    var stopwatch = Stopwatch.StartNew();

    var payload = new Payload
    {
      Method = context.Request.Method,
      Url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}",
      Path = context.Request.Path,
      RequestHeaders = context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
    };

    // Capture the request body
    var requestBody = await CaptureRequestBody(context);
    payload.Request = requestBody;
    payload.RequestBodyUrl = await IndexDocumentAndGetUrl(requestBody, "request");

    // Capture the original response stream
    var originalBodyStream = context.Response.Body;

    using (var responseBody = new MemoryStream())
    {
      context.Response.Body = responseBody;

      // Call the next middleware in the pipeline
      await next(context);

      // Stop the stopwatch to calculate time taken in seconds with millisecond precision
      stopwatch.Stop();
      payload.TimeTakenSec = stopwatch.Elapsed.TotalSeconds;

      // Capture the response details
      payload.StatusCode = context.Response.StatusCode;
      payload.ResponseHeaders = context.Response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());

      // Capture the response body
      var responseBodyContent = await CaptureResponseBody(context);
      payload.Response = responseBodyContent;
      payload.ResponseBodyUrl = await IndexDocumentAndGetUrl(responseBodyContent, "response");

      // Log the payload with URLs to the request/response contents
      using (LogContext.PushProperty("Method", payload.Method))
      using (LogContext.PushProperty("Url", payload.Url))
      using (LogContext.PushProperty("Path", payload.Path))
      using (LogContext.PushProperty("RequestHeaders", payload.RequestHeaders))
      using (LogContext.PushProperty("request", payload.Request))
      using (LogContext.PushProperty("RequestBodyUrl", payload.RequestBodyUrl))
      using (LogContext.PushProperty("ResponseHeaders", payload.ResponseHeaders))
      using (LogContext.PushProperty("response", payload.Response))
      using (LogContext.PushProperty("ResponseBodyUrl", payload.ResponseBodyUrl))
      using (LogContext.PushProperty("StatusCode", payload.StatusCode))
      using (LogContext.PushProperty("TimeTakenSec", payload.TimeTakenSec))
      {
        Log.Information("HTTP Request and Response logged with Elasticsearch URLs: {@Payload}", payload);
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

  private async Task<string> IndexDocumentAndGetUrl(string content, string documentType)
  {
    var document = new
    {
      Timestamp = DateTime.UtcNow,
      Type = documentType,
      Content = content
    };

    var response = await elasticClient.IndexAsync(document, idx => idx.Index(Constants.SerilogIndex.RequestResponse));

    if (!response.IsValid)
    {
      throw new Exception($"Failed to index document in Elasticsearch: {response.DebugInformation}");
    }

    var elasticUrl = configuration["ElasticConfiguration:Uri"];
    // Construct the URL to view only the 'content' field of the document
    string documentUrl = $"{elasticUrl}request-response-logs/_doc/{response.Id}?_source=content"; // Adjust the URL as needed

    return documentUrl;
  }
}
