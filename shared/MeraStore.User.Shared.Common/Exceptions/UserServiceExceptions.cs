using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class UserServiceExceptions
{
  public class UserNotFoundException(string message) : BaseAppException(ServiceIdentifiers.UserService,
    EventCodes.ResourceNotFound, ErrorCodes.NotFoundError, message);

  public class UserAlreadyExistsException(string message) : BaseAppException(ServiceIdentifiers.UserService,
    EventCodes.ResourceConflict, ErrorCodes.ConflictError, message);
}