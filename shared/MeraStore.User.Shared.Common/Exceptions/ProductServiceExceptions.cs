﻿using System.Net;
using MeraStore.User.Shared.Common.ErrorsCodes;

namespace MeraStore.User.Shared.Common.Exceptions;

public class ProductServiceExceptions
{
  public class ProductNotFoundException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ProductService),
    EventCodeProvider.GetEventCode(Constants.EventCodes.ResourceNotFound),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.NotFoundError),
    HttpStatusCode.NotFound, message);

  public class ProductCreationException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ProductService),
    EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidRequest),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFieldError),
    HttpStatusCode.BadRequest, message);

  public class ProductUpdateException(string message) : BaseAppException(
    ServiceProvider.GetServiceCode(Constants.ServiceIdentifiers.ProductService),
    EventCodeProvider.GetEventCode(Constants.EventCodes.InvalidRequest),
    ErrorCodeProvider.GetErrorCode(Constants.ErrorCodes.InvalidFieldError),
    HttpStatusCode.BadRequest, message);
}