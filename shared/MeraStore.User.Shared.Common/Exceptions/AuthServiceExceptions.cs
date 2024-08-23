using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class AuthServiceExceptions
{
  public class AuthenticationException(string message) : BaseAppException(ServiceIdentifiers.AuthService,
    EventCodes.UnauthorizedAccess, ErrorCodes.UnauthorizedError, message);

  public class AuthorizationException(string message) : BaseAppException(ServiceIdentifiers.AuthService,
    EventCodes.Forbidden, ErrorCodes.ForbiddenError, message);
}