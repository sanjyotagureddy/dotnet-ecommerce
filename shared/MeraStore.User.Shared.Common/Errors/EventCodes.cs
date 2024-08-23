using MeraStore.User.Shared.Common.Resources;

namespace MeraStore.User.Shared.Common.Errors;

public class EventCodes
{
  public static string ServiceUnavailable => EventCode.ServiceUnavailable;
  public static string ResourceNotFound => EventCode.ResourceNotFound;
  public static string InvalidRequest => EventCode.InvalidRequest;
  public static string UnauthorizedAccess => EventCode.UnauthorizedAccess;
  public static string Forbidden => EventCode.Forbidden;
  public static string ResourceConflict => EventCode.ResourceConflict;
  public static string InternalServerError => EventCode.InternalServerError;
  public static string DataValidationError => EventCode.DataValidationError;
  public static string Timeout => EventCode.Timeout;
  public static string ServiceError => EventCode.ServiceError;
  public static string NotImplemented => EventCode.NotImplemented;
  public static string ApiKeyMissing => EventCode.ApiKeyMissing;
  public static string TokenExpired => EventCode.TokenExpired;
  public static string FeatureNotSupported => EventCode.FeatureNotSupported;
  public static string RateLimitExceeded => EventCode.RateLimitExceeded;
  public static string NetworkError => EventCode.NetworkError;
  public static string DataIntegrityViolation => EventCode.DataIntegrityViolation;
  public static string MissingParameter => EventCode.MissingParameter;
  public static string InvalidParameter => EventCode.InvalidParameter;
  public static string RequestTimeout => EventCode.RequestTimeout;
  public static string UnsupportedMediaType => EventCode.UnsupportedMediaType;
}