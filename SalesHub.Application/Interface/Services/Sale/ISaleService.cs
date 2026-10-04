using SalesHub.Application.DTOs.Sale;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface ISaleService
    {
        Task<ApiResponse<SaleDto>> CreateAsync(CreateSaleDto dto);

        Task<ApiResponse<SaleDto>> UpdateAsync(int id, UpdateSaleDto updateDTO);
        Task<ApiResponse<List<SaleDto>>> GetAllAsync();

        Task<ApiResponse<SaleDto>> GetByIdAsync(int id);

        Task<ApiResponse<List<SaleDto>>> GetByCustomerIdAsync( int customerId);

        Task<ApiResponse<List<SaleDto>>> GetByUserIdAsync(int userId);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
