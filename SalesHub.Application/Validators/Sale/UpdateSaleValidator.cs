using FluentValidation;
using SalesHub.Application.DTOs.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Validators.Sale
{
    public class UpdateSaleValidator
        : AbstractValidator<UpdateSaleDto>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than zero.");

            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(x => x.Details)
                .NotEmpty()
                .WithMessage("A sale must contain at least one detail.");

            RuleForEach(x => x.Details)
                .SetValidator(new UpdateSaleDetailValidator());

            RuleFor(x => x.Details)
                .Must(details =>
                    details.Select(d => d.ProductId)
                        .Distinct()
                        .Count() == details.Count)
                .WithMessage("A product cannot appear more than once in a sale.");
        }
    }
}