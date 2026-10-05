using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.DTOs.InventoryMovement;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Services;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;

namespace SalesHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementService _inventoryMovementService; 
        public InventoryMovementController(IInventoryMovementService inventoryMovementService)
        { 
            _inventoryMovementService = inventoryMovementService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<InventoryMovementDto>>>> GetAll()
        { 
            var response = await _inventoryMovementService.GetAllAsync();
            return StatusCode(response.StatusCode, response); 
        }

        [HttpGet("{id:int}")] 
        public async Task<ActionResult<ApiResponse<InventoryMovementDto?>>> GetById(int id)
        { 
            var response = await _inventoryMovementService.GetByIdAsync(id); 
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<InventoryMovementDto>>> Create([FromBody] CreateInventoryMovementDto createDTO) 
        { 
            var response = await _inventoryMovementService.CreateAsync(createDTO);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id:int}")] 
        public async Task<ActionResult<ApiResponse<InventoryMovementDto>>> Update(int id, [FromBody] UpdateInventoryMovementDto updateDTO)
        { 
            var response = await _inventoryMovementService.UpdateAsync(id, updateDTO);
            return StatusCode(response.StatusCode, response); 
        }

        [HttpDelete("{id:int}")] 
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var response = await _inventoryMovementService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult<ApiResponse<List<InventoryMovementDto>>>> GetByProductId(int productId)
        { 
            var response = await _inventoryMovementService.GetByProductIdAsync(productId);
            return StatusCode(response.StatusCode, response); 
        }

        [HttpGet("user/{userId:int}")] 
        public async Task<ActionResult<ApiResponse<List<InventoryMovementDto>>>> GetByUserId(int userId)
        {
            var response = await _inventoryMovementService.GetByUserIdAsync(userId);
            return StatusCode(response.StatusCode, response); 
        }
    }
}
