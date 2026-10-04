using FluentValidation;
using SalesHub.Application.DTOs.Sale.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Validators.Sale
{
    public class UpdateSaleDetailValidator
        : AbstractValidator<UpdateSaleDetailDto>
    {
        public UpdateSaleDetailValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("ProductId must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}