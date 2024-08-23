using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class CartServiceExceptions
{
  public class CartNotFoundException(string message) : BaseAppException(ServiceIdentifiers.CartService,
    EventCodes.ResourceNotFound, ErrorCodes.NotFoundError, message);

  public class CartItemNotFoundException(string message) : BaseAppException(ServiceIdentifiers.CartService,
    EventCodes.ResourceNotFound, ErrorCodes.NotFoundError, message);
}