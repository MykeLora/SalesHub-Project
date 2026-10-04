using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.DTOs.Customer;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;

namespace SalesHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CustomerDto>>>> GetAll()
        {
            var response = await _customerService.GetAllAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> GetById(int id)
        {
            var response = await _customerService.GetByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("national-id/{nationalId}")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> GetByNationalId(string nationalId)
        {
            var response = await _customerService
                .GetByNationalIdAsync(nationalId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> Create(
            [FromBody] CreateCustomerDto dto)
        {
            var response = await _customerService.CreateAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<CustomerDto>>> Update(
            int id,
            [FromBody] UpdateCustomerDto dto)
        {
            var response = await _customerService.UpdateAsync(id, dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _customerService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}

