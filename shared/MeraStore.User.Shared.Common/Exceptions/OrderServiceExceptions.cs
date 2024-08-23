using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class OrderServiceExceptions
{
  public class OrderNotFoundException(string message) : BaseAppException(ServiceIdentifiers.OrderService,
    EventCodes.ResourceNotFound, ErrorCodes.NotFoundError, message);

  public class OrderProcessingException(string message) : BaseAppException(ServiceIdentifiers.OrderService,
    EventCodes.InternalServerError, ErrorCodes.InternalServerError, message);
}