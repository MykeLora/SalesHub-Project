using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.DTOs.Sale.SaleDetail;
using SalesHub.Application.Interface.Services.Sale;
using SalesHub.Application.Wrappers;

namespace SalesHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ControllerBase
    {
        private readonly ISaleDetailService _saleDetailService;

        public SaleDetailController(
            ISaleDetailService saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<SaleDetailDto>>>>
            GetAll()
        {
            var response =
                await _saleDetailService.GetAllAsync();

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<SaleDetailDto>>>
            GetById(int id)
        {
            var response =
                await _saleDetailService.GetByIdAsync(id);

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<SaleDetailDto>>>
            Create(CreateSaleDetailDto dto)
        {
            var response =
                await _saleDetailService.CreateAsync(dto);

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<SaleDetailDto>>>
            Update(
                int id,
                UpdateSaleDetailDto dto)
        {
            var response =
                await _saleDetailService.UpdateAsync(id, dto);

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>>
            Delete(int id)
        {
            var response =
                await _saleDetailService.DeleteAsync(id);

            return StatusCode(
                response.StatusCode,
                response);
        }
    }
}
