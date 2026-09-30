using AutoMapper;
using SalesHub.Application.DTOs.Product;
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
    public class ProductService : GenericService<CreateProductDto, UpdateProductDto, Product, ProductDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfwork _UnitOfWork;
        public ProductService(IProductRepository productRepository,
            IMapper mapper,
            IUnitOfwork unitOfwork)
            : base(productRepository, mapper, unitOfwork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _UnitOfWork = unitOfwork;
        }

        public async Task<ApiResponse<List<ProductDto>>> GetActiveProductAsync()
        {
            try
            {
                var product = await _productRepository.GetActiveProductsAsync();

                var response = _mapper.Map<List<ProductDto>>(product);

                return new ApiResponse<List<ProductDto>>(
                    200,
                    response);

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProductDto>>(
                    500,
                    $"An error ocurred while retrievinf active products: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<ProductDto>>> GetByCategoryIdAsync(int categoryId)
        {
            try
            {
                var products =
                    await _productRepository.GetByCategoryIdAsync(categoryId);

                var response = _mapper.Map<List<ProductDto>>(products);


                return new ApiResponse<List<ProductDto>>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProductDto>>(
                    500,
                    $"An error occurred while retrieving products: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductDto>> GetBySkuAsync(string sku)
        {
            try
            {
                var product = await _productRepository.GetByCodeAsync(sku);

                if (product is null)
                {
                    return new ApiResponse<ProductDto>(
                        404,
                        "Product not found.");
                }

                var response = _mapper.Map<ProductDto>(product);

                return new ApiResponse<ProductDto>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDto>(
                    500,
                    $"An error occurred while retrieving the product: {ex.Message}");
            }
        }
    }
}