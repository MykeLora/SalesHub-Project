using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.DTOs.Product;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;

namespace SalesHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<ProductDto>>>> GetAll()
        {
            var response = await _productService.GetAllAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(int id)
        {
            var response = await _productService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("sku/{sku}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetBySku(
            string sku)
        {
            var response = await _productService.GetBySkuAsync(sku);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<List<ProductDto>>>> GetActive()
        {
            var response = await _productService.GetActiveProductAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<ApiResponse<List<ProductDto>>>> GetByCategory(
            int categoryId)
        {
            var response = await _productService.GetByCategoryIdAsync(categoryId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Create(
            [FromBody] CreateProductDto dto)
        {
            var response = await _productService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> Update(
            int id,
            [FromBody] UpdateProductDto dto)
        {
            var response = await _productService.UpdateAsync(id, dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _productService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}
