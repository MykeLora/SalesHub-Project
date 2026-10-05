using AutoMapper;
using SalesHub.Application.DTOs.InventoryMovement;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Mapping
{
    public class InventoryMovementProfile : Profile
    {
        public InventoryMovementProfile()
        {
            CreateMap<InventoryMovement, InventoryMovementDto>();
            CreateMap<CreateInventoryMovementDto, InventoryMovement>();
            CreateMap<UpdateInventoryMovementDto, InventoryMovement>();
        }
    }
}
