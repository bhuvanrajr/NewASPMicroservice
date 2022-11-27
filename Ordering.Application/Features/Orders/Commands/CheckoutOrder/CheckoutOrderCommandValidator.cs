using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is Required")
                .NotNull().WithMessage("{UserName} is Required")
                .MaximumLength(50).WithMessage("{UserName} length cannot exceed 50 characters");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} is Required")
                .NotNull().WithMessage("{EmailAddress} is Required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is Required")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero");
        }

    }
}
