using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.DTOs.Category;
using SalesHub.Application.DTOs.Product;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;

namespace SalesHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CategoryDto>>>> GetAll()
        {
            var response = await _categoryService.GetAllAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);

            return StatusCode(Response.StatusCode, response);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> GetByName(string name)
        {
            var response = await _categoryService.GetByNameAsync(name);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Active")]
        public async Task<ActionResult<ApiResponse<List<CategoryDto>>>> GetActive()
        {
            var response = await _categoryService.GetActiveCategoriesAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> Create(
            [FromBody] CreateCategoryDto dto)
        {
            var response = await _categoryService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<CategoryDto>>> Update(
            int id,
            [FromBody] UpdateCategoryDto dto)
        {
            var response = await _categoryService.UpdateAsync(id, dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }

    }
}
