using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.ID)
                .NotEmpty().WithMessage("{ID} is Required")
                .NotEqual(0).WithMessage("{ID} is Required");

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
