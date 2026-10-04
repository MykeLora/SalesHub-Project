using AutoMapper;
using SalesHub.Application.DTOs.Sale.SaleDetail;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Mapping
{
    public class SaleDetailProfile : Profile
    {
        public SaleDetailProfile()
        {
            CreateMap<SaleDetail, SaleDetailDto>();

            CreateMap<CreateSaleDetailDto, SaleDetail>();

            CreateMap<UpdateSaleDetailDto, SaleDetail>();
        }
    }
}
