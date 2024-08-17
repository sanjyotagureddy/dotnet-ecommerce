namespace MeraStore.User.Shared.Common;

public static class Constants
{
  public static readonly string ApplicationName = "ApplicationName";
  public static class ServiceIdentifiers
  {
    public static readonly string User = "merastore-user-api";
    public static readonly string Product = "merastore-product-api";
  }

  public static class Headers
  {
    public static readonly string CorrelationId = "x-correlation-id";
  }

  public static class SerilogIndex
  {
    public static readonly string RequestResponse = "request-response-logs";
    public static readonly string User = "user";
  }
}