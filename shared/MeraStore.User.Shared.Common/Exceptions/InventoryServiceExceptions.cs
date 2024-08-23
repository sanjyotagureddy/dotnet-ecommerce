using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class InventoryServiceExceptions
{
  public class InventoryNotFoundException(string message) : BaseAppException(ServiceIdentifiers.InventoryService,
    EventCodes.ResourceNotFound, ErrorCodes.NotFoundError, message);

  public class InsufficientInventoryException(string message) : BaseAppException(ServiceIdentifiers.InventoryService,
    EventCodes.InvalidRequest, ErrorCodes.BadRequestError, message);
}