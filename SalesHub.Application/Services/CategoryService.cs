using AutoMapper;
using SalesHub.Application.DTOs.Category;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Services
{
    public class CategoryService : GenericService<CreateCategoryDto, UpdateCategoryDto, Category, CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository , IMapper mapper, IUnitOfwork unitOfwork)
            : base(categoryRepository, mapper,unitOfwork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            
        }
        public async Task<ApiResponse<CategoryDto>> GetByNameAsync(string name)
        {
            try
            {
                var category =
                    await _categoryRepository.GetByNameAsync(name);

                if (category is null)
                {
                    return new ApiResponse<CategoryDto>(
                        404,
                        "Category not found.");
                }

                var response =
                    _mapper.Map<CategoryDto>(category);

                return new ApiResponse<CategoryDto>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryDto>(
                    500,
                    $"An error occurred while retrieving the category: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<CategoryDto>>> GetActiveCategoriesAsync()
        {
            try
            {
                var categories =
                    await _categoryRepository.GetActiveCategoriesAsync();

                var response =
                    _mapper.Map<List<CategoryDto>>(categories);

                return new ApiResponse<List<CategoryDto>>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryDto>>(
                    500,
                    $"An error occurred while retrieving active categories: {ex.Message}");
            }
        }
    }
}
