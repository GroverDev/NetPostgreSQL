using FluentValidation;

namespace Store.Application.Validators.Product;

public class ProductValidator: AbstractValidator<ProductRequestDto>
{
    public ProductValidator()
    {
        RuleFor(x=>x.ProductName)
        .NotNull().WithMessage("El campo Nombre no puede ser nulo")
        .NotEmpty().WithMessage("Tampoco peude ser vacio");;
    }
}
