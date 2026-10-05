using SalesHub.Application.DTOs.InventoryMovement;
using SalesHub.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface IInventoryMovementService
    {
        Task<ApiResponse<List<InventoryMovementDto>>> GetAllAsync(); 
        Task<ApiResponse<InventoryMovementDto?>> GetByIdAsync(int id); 
        Task<ApiResponse<InventoryMovementDto>> CreateAsync(CreateInventoryMovementDto createDTO); 
        Task<ApiResponse<InventoryMovementDto>> UpdateAsync(int id, UpdateInventoryMovementDto updateDTO);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<List<InventoryMovementDto>>> GetByProductIdAsync(int productId);
        Task<ApiResponse<List<InventoryMovementDto>>> GetByUserIdAsync(int userId);   
    }
}
