using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class CommonExceptions
{
  public class ApiCommunicationException(string message) : BaseAppException(ServiceIdentifiers.ApiGateway,
    EventCodes.ServiceError, ErrorCodes.BadGatewayError, message);

  public class EventBusCommunicationException(string message) : BaseAppException(ServiceIdentifiers.EventBus,
    EventCodes.ServiceError, ErrorCodes.ServiceUnavailableError, message);

  public class ValidationException(string message) : BaseAppException(ServiceIdentifiers.DataValidation,
    EventCodes.DataValidationError, ErrorCodes.ValidationError, message);

  public class BusinessRuleException(string message) : BaseAppException(ServiceIdentifiers.DataValidation,
    EventCodes.ResourceConflict, ErrorCodes.UnprocessableEntityError, message);

  public class ConfigurationException(string message) : BaseAppException(ServiceIdentifiers.Configuration,
    EventCodes.ServiceError, ErrorCodes.InternalServerError, message);

  public class DatabaseConnectionException(string message) : BaseAppException(ServiceIdentifiers.Database,
    EventCodes.ServiceError, ErrorCodes.InternalServerError, message);

  public class DataNotFoundException(string message) : BaseAppException(ServiceIdentifiers.Database,
    EventCodes.ResourceNotFound, ErrorCodes.DataNotFoundError, message);

  public class DataIntegrityException(string message) : BaseAppException(ServiceIdentifiers.Database,
    EventCodes.DataIntegrityViolation, ErrorCodes.InternalServerError, message);

  public class SecurityException(string message) : BaseAppException(ServiceIdentifiers.Security,
    EventCodes.UnauthorizedAccess, ErrorCodes.InternalServerError, message);

  public class PermissionDeniedException(string message) : BaseAppException(ServiceIdentifiers.Security,
    EventCodes.Forbidden, ErrorCodes.ForbiddenError, message);
}