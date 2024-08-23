using MeraStore.User.Shared.Common.Errors;

namespace MeraStore.User.Shared.Common.Exceptions;

[ExcludeFromCodeCoverage]
public class ProductServiceExceptions
{
  public class ProductNotFoundException(string message) : BaseAppException(ServiceIdentifiers.ProductService,
    EventCodes.ResourceNotFound, ErrorCodes.NotFoundError, message);

  public class ProductOutOfStockException(string message) : BaseAppException(ServiceIdentifiers.ProductService,
    EventCodes.InvalidRequest, ErrorCodes.BadRequestError, message);
}