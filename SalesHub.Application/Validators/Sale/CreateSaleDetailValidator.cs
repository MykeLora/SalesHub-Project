using FluentValidation;
using SalesHub.Application.DTOs.Sale.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Validators.Sale
{
    public class CreateSaleDetailValidator : AbstractValidator<CreateSaleDetailDto>
    {
        public CreateSaleDetailValidator()
        {

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        }
    }
}
