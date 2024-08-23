namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class BaseAppException : Exception
{
  public string EventCode { get; }
  public string ServiceIdentifier { get; }
  public string ErrorCode { get; }

  public string FullErrorCode => $"{ServiceIdentifier}-{ErrorCode}";

  public BaseAppException(string serviceIdentifier, string eventCode, string errorCode, string message)
    : base(message)
  {
    ServiceIdentifier = serviceIdentifier;
    EventCode = eventCode;
    ErrorCode = errorCode;
  }

  // Additional constructor for wrapping generic exceptions
  public BaseAppException(string serviceIdentifier, string eventCode, string errorCode, Exception innerException)
    : base(innerException?.Message, innerException)
  {
    ServiceIdentifier = serviceIdentifier;
    EventCode = eventCode;
    ErrorCode = errorCode;
  }
}
