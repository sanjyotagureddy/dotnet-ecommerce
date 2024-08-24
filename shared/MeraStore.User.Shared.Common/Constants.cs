namespace MeraStore.User.Shared.Common;

public static class Constants
{
  public static readonly string ApplicationName = "ApplicationName";
  public static class ApplicationNames
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
  }

  public static class ServiceIdentifiers
  {
    public static readonly string General = "general";
    public static readonly string ApiGateway = "api-gateway";
    public static readonly string AuthService = "auth-service";
    public static readonly string CartService = "cart-service";
    public static readonly string Configuration = "configuration";
    public static readonly string Database = "database";
    public static readonly string DataValidation = "data-validation";
    public static readonly string EventBus = "event-bus";
    public static readonly string InventoryService = "inventory-service";
    public static readonly string Network = "network";
    public static readonly string Operational = "operational";
    public static readonly string OrderService = "order-service";
    public static readonly string ProductService = "product-service";
    public static readonly string Security = "security";
    public static readonly string UserService = "user-service";
  }

  public static class EventCodes
  {
    public const string ApiKeyMissing = "ApiKeyMissing";
    public const string DataIntegrityViolation = "DataIntegrityViolation";
    public const string DataValidationError = "DataValidationError";
    public const string FeatureNotSupported = "FeatureNotSupported";
    public const string Forbidden = "Forbidden";
    public const string InternalServerError = "InternalServerError";
    public const string InvalidParameter = "InvalidParameter";
    public const string InvalidRequest = "InvalidRequest";
    public const string MissingParameter = "MissingParameter";
    public const string NetworkError = "NetworkError";
    public const string NotImplemented = "NotImplemented";
    public const string RateLimitExceeded = "RateLimitExceeded";
    public const string RequestTimeout = "RequestTimeout";
    public const string ResourceConflict = "ResourceConflict";
    public const string ResourceNotFound = "ResourceNotFound";
    public const string ServiceError = "ServiceError";
    public const string ServiceUnavailable = "ServiceUnavailable";
    public const string Timeout = "Timeout";
    public const string TokenExpired = "TokenExpired";
    public const string UnauthorizedAccess = "UnauthorizedAccess";
    public const string UnsupportedMediaType = "UnsupportedMediaType";
    public const string DeserializationError = "DeserializationError";
    public const string InvalidOperation = "InvalidOperation";
    public const string OperationFailed = "OperationFailed";
    public const string SerializationError = "SerializationError";
  }

  public static class ErrorCodes
  {
    public const string BadGatewayError = "BadGatewayError";
    public const string BadRequestError = "BadRequestError";
    public const string ConflictError = "ConflictError";
    public const string DataNotFoundError = "DataNotFoundError";
    public const string ForbiddenError = "ForbiddenError";
    public const string InputError = "InputError";
    public const string InternalServerError = "InternalServerError";
    public const string InvalidFieldError = "InvalidFieldError";
    public const string InvalidFormatError = "InvalidFormatError";
    public const string MissingFieldError = "MissingFieldError";
    public const string NotFoundError = "NotFoundError";
    public const string NotImplementedError = "NotImplementedError";
    public const string RequestError = "RequestError";
    public const string ServiceUnavailableError = "ServiceUnavailableError";
    public const string TooManyRequestsError = "TooManyRequestsError";
    public const string UnauthorizedError = "UnauthorizedError";
    public const string UnprocessableEntityError = "UnprocessableEntityError";
    public const string ValidationError = "ValidationError";
  }

}