namespace MeraStore.User.Shared.Common.Errors;

public static class ErrorCodes
{
  public static string NotFoundError => Resources.ErrorCodes.NotFoundError;
  public static string BadRequestError => Resources.ErrorCodes.BadRequestError;
  public static string ConflictError => Resources.ErrorCodes.ConflictError;
  public static string InternalServerError => Resources.ErrorCodes.InternalServerError;
  public static string UnauthorizedError => Resources.ErrorCodes.UnauthorizedError;
  public static string ForbiddenError => Resources.ErrorCodes.ForbiddenError;
  public static string BadGatewayError => Resources.ErrorCodes.BadGatewayError;
  public static string ServiceUnavailableError => Resources.ErrorCodes.ServiceUnavailableError;
  public static string UnprocessableEntityError => Resources.ErrorCodes.UnprocessableEntityError;
  public static string NotImplementedError => Resources.ErrorCodes.NotImplementedError;
  public static string TooManyRequestsError => Resources.ErrorCodes.TooManyRequestsError;
  public static string MissingFieldError => Resources.ErrorCodes.MissingFieldError;
  public static string InvalidFieldError => Resources.ErrorCodes.InvalidFieldError;
  public static string InvalidFormatError => Resources.ErrorCodes.InvalidFormatError;
  public static string ValidationError => Resources.ErrorCodes.ValidationError;
  public static string InputError => Resources.ErrorCodes.InputError;
  public static string RequestError => Resources.ErrorCodes.RequestError;
  public static string DataNotFoundError => Resources.ErrorCodes.DataNotFoundError;
}