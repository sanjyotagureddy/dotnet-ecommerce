namespace MeraStore.Shared.Common.Logging.Exceptions;

public class BaseAppException(string serviceIdentifier, string errorCode, string message) : Exception(message)
{
  public string ServiceIdentifier { get; } = serviceIdentifier;
  public string ErrorCode { get; } = errorCode;

  public string FullErrorCode => $"{ServiceIdentifier}-{ErrorCode}";
}