using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.DTOs.Sale;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Interface.Services.Sale;
using SalesHub.Application.Wrappers;

namespace SalesHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<SaleDto>>>> GetAll()
        {
            var response = await _saleService.GetAllAsync();

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<SaleDto>>> GetById(int id)
        {
            var response = await _saleService.GetByIdAsync(id);

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpGet("customer/{customerId:int}")]
        public async Task<ActionResult<ApiResponse<List<SaleDto>>>> GetByCustomer(
            int customerId)
        {
            var response = await _saleService
                .GetByCustomerIdAsync(customerId);

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<ApiResponse<List<SaleDto>>>> GetByUser(
            int userId)
        {
            var response = await _saleService
                .GetByUserIdAsync(userId);

            return StatusCode(
                response.StatusCode,
                response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<SaleDto>>> Create(
            [FromBody] CreateSaleDto dto)
        {
            var response = await _saleService.CreateAsync(dto);

            return StatusCode(
                response.StatusCode,
                response);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<SaleDto>>> Update(
            int id,
            [FromBody] UpdateSaleDto updateDTO)
                {
                    var response = await _saleService.UpdateAsync(id, updateDTO);

                    return StatusCode(response.StatusCode, response);
                }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _saleService.DeleteAsync(id);

            return StatusCode(
                response.StatusCode,
                response);
        }

    }
}
