using AutoMapper;
using SalesHub.Application.DTOs.Sale.SaleDetail;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services.Sale;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Services
{
    public class SaleDetailService : GenericService<CreateSaleDetailDto, UpdateSaleDetailDto, SaleDetail, SaleDetailDto>, ISaleDetailService
    {
        public SaleDetailService(ISaleDetailRepository repo, IMapper mapper, IUnitOfwork unitOfWork)
            : base(repo, mapper, unitOfWork)
        {
        }
    }
}
