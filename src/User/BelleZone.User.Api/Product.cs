using FluentValidation;

namespace BelleZone.User.Api;

public class Product
{
  public string Name { get; set; }
  public double Price { get; set; }
  public string? Description { get; set; }
}


// Assuming you have FluentValidation set up for your models.
public class ProductValidator : AbstractValidator<Product>
{
  public ProductValidator()
  {
    RuleFor(x => x.Name).NotEmpty().WithErrorCode("01").WithMessage("Name is required.");
    RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
  }
}
