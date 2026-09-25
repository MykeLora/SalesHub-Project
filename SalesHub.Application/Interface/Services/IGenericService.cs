using SalesHub.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface IGenericService<CreateDTO, UpdateDTO, Entity, Response>
        where CreateDTO : class
        where UpdateDTO : class
        where Entity : class
        where Response : class 
    {
        Task<ApiResponse<Response>> CreateAsync(CreateDTO createDTO);
        Task<ApiResponse<Response>> UpdateAsync(int id,UpdateDTO updateDTO);
        Task<ApiResponse<Response?>> GetByIdAsync(int id);
        Task<ApiResponse<List<Response>>> GetAllAsync();
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
