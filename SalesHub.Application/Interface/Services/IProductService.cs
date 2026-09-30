using SalesHub.Application.DTOs.Product;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface IProductService : IGenericService<CreateProductDto,UpdateProductDto,Product,ProductDto>
    {
        Task<ApiResponse<ProductDto>> GetBySkuAsync(string sku);
        Task<ApiResponse<List<ProductDto>>> GetActiveProductAsync();
        Task<ApiResponse<List<ProductDto>>> GetByCategoryIdAsync(int categoryId);
    }
}
