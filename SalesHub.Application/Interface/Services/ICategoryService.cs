using SalesHub.Application.DTOs.Category;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface ICategoryService : IGenericService<CreateCategoryDto, UpdateCategoryDto, Category, CategoryDto>
    {
        Task<ApiResponse<CategoryDto>> GetByNameAsync(string name);
        Task<ApiResponse<List<CategoryDto>>> GetActiveCategoriesAsync();
    }
}
