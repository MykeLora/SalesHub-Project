using SalesHub.Application.DTOs.Sale.SaleDetail;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services.Sale
{
    public interface ISaleDetailService : IGenericService<CreateSaleDetailDto, UpdateSaleDetailDto, SaleDetail, SaleDetailDto>
    {
    }
}
