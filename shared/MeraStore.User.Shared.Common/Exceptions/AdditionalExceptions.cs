using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class AdditionalExceptions
{
  public class ServiceTimeoutException(string message) : BaseAppException(ServiceIdentifiers.ApiGateway,
    EventCodes.Timeout, ErrorCodes.BadGatewayError, message);

  public class ServiceUnavailableException(string message) : BaseAppException(ServiceIdentifiers.ApiGateway,
    EventCodes.ServiceUnavailable, ErrorCodes.ServiceUnavailableError, message);

  public class DuplicateRecordException(string message) : BaseAppException(ServiceIdentifiers.Database,
    EventCodes.ResourceConflict, ErrorCodes.ConflictError, message);

  public class InvalidDataFormatException(string message) : BaseAppException(ServiceIdentifiers.DataValidation,
    EventCodes.InvalidParameter, ErrorCodes.InvalidFormatError, message);

  public class MissingConfigurationException(string message) : BaseAppException(ServiceIdentifiers.Configuration,
    EventCodes.ResourceNotFound, ErrorCodes.MissingFieldError, message);

  public class InvalidEnvironmentConfigurationException(string message) : BaseAppException(
    ServiceIdentifiers.Configuration, EventCodes.InvalidRequest, ErrorCodes.InvalidFieldError, message);

  public class ApiKeyMissingException(string message) : BaseAppException(ServiceIdentifiers.Security,
    EventCodes.ApiKeyMissing, ErrorCodes.UnauthorizedError, message);

  public class TokenExpiredException(string message) : BaseAppException(ServiceIdentifiers.Security,
    EventCodes.TokenExpired, ErrorCodes.UnauthorizedError, message);

  public class FeatureNotSupportedException(string message) : BaseAppException(ServiceIdentifiers.Operational,
    EventCodes.FeatureNotSupported, ErrorCodes.NotImplementedError, message);

  public class ServiceLimitExceededException(string message) : BaseAppException(ServiceIdentifiers.Operational,
    EventCodes.RateLimitExceeded, ErrorCodes.TooManyRequestsError, message);

  public class NetworkFailureException(string message) : BaseAppException(ServiceIdentifiers.Network,
    EventCodes.NetworkError, ErrorCodes.BadGatewayError, message);

  public class ConnectionRefusedException(string message) : BaseAppException(ServiceIdentifiers.Network,
    EventCodes.NetworkError, ErrorCodes.BadGatewayError, message);
}