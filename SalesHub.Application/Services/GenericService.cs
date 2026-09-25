using AutoMapper;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;

namespace SalesHub.Application.Services;

public class GenericService<CreateDTO, UpdateDTO, Entity, Response>
    : IGenericService<CreateDTO, UpdateDTO, Entity, Response>
    where CreateDTO : class
    where UpdateDTO : class
    where Entity : class
    where Response : class
{
    private readonly IGenericRepository<Entity> _repo;
    private readonly IMapper _mapper;
    private readonly IUnitOfwork _unitOfWork;

    public GenericService(
        IGenericRepository<Entity> repo,
        IMapper mapper,
        IUnitOfwork unitOfWork)
    {
        _repo = repo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public virtual async Task<ApiResponse<Response>> CreateAsync(
        CreateDTO createDTO)
    {
        try
        {
            var entity = _mapper.Map<Entity>(createDTO);

            var createdEntity = _repo.AddAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<Response>(createdEntity);

            return new ApiResponse<Response>(201, response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<Response>(
                500,
                $"An error occurred while creating the entity: {ex.Message}");
        }
    }

    public virtual async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var result = await _repo.DeleteAsync(id);

            if (!result)
            {
                return new ApiResponse<bool>(
                    404,
                    "Entity not found.");
            }

            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<bool>(200, true);
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>(
                500,
                $"An error occurred while deleting the entity: {ex.Message}");
        }
    }

    public virtual async Task<ApiResponse<List<Response>>> GetAllAsync()
    {
        try
        {
            var entities = await _repo.GetAllAsync();

            var response = _mapper.Map<List<Response>>(entities);

            return new ApiResponse<List<Response>>(
                200,
                response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<Response>>(
                500,
                $"An error occurred while retrieving the entities: {ex.Message}");
        }
    }

    public virtual async Task<ApiResponse<Response?>> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity is null)
            {
                return new ApiResponse<Response?>(
                    404,
                    "Entity not found.");
            }

            var response = _mapper.Map<Response>(entity);

            return new ApiResponse<Response?>(
                200,
                response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<Response?>(
                500,
                $"An error occurred while retrieving the entity: {ex.Message}");
        }
    }

    public virtual async Task<ApiResponse<Response>> UpdateAsync(
        int id,
        UpdateDTO updateDTO)
    {
        try
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity is null)
            {
                return new ApiResponse<Response>(
                    404,
                    "Entity not found.");
            }

            _mapper.Map(updateDTO, entity);

            await _repo.UpdateAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<Response>(entity);

            return new ApiResponse<Response>(
                200,
                response);
        }
        catch (Exception ex)
        {
            return new ApiResponse<Response>(
                500,
                $"An error occurred while updating the entity: {ex.Message}");
        }
    }
}