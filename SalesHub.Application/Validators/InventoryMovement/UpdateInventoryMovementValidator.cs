using FluentValidation;
using SalesHub.Application.DTOs.InventoryMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Validators.InventoryMovement
{
    public class UpdateInventoryMovementValidator : AbstractValidator<UpdateInventoryMovementDto>
    {
        public UpdateInventoryMovementValidator() 
        {
            
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero."); 

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Reason is required.")
                .MaximumLength(300)
                .WithMessage("Reason cannot exceed 300 characters."); 

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid inventory movement type."); 
        } 
    }
}
