using AutoMapper;
using SalesHub.Application.DTOs.InventoryMovement;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using SalesHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IInventoryMovementRepository _movementRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper; 
        private readonly IUnitOfwork _unitOfWork;
        public InventoryMovementService(
            IInventoryMovementRepository movementRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IMapper mapper, IUnitOfwork unitOfWork)

        {
            _movementRepository = movementRepository; 
            _productRepository = productRepository;
            _mapper = mapper; _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<InventoryMovementDto>> CreateAsync(CreateInventoryMovementDto createDTO)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(createDTO.ProductId);

                if(product is null)
                {
                    return new ApiResponse<InventoryMovementDto>(404, 
                        "Product not found.");
                }   

                if(!product.Status.Equals(ProductStatus.Active))
                {
                    return new ApiResponse<InventoryMovementDto>(400, 
                        "Product is not active.");
                }
                var previousStock = product.Stock;

                ApplyMovement(
                    product,
                    createDTO.Type,
                    createDTO.Quantity);

                var movement = _mapper.Map<InventoryMovement>(
                    createDTO);

                movement.PreviousStock = previousStock;
                movement.ResultingStock = product.Stock;

                await SaveMovementAsync(movement, product);

                var response = _mapper.Map<InventoryMovementDto>(
                    movement);

                return new ApiResponse<InventoryMovementDto>(
                    201,
                    response);

            }
            catch (InvalidOperationException ex)
            {
                return new ApiResponse<InventoryMovementDto>(
                    400,
                    ex.Message);
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryMovementDto>(
            500,
            $"An error occurred while creating the inventory movement: {ex.Message}");
            }
        }

        public async Task<ApiResponse<InventoryMovementDto>> UpdateAsync(int id, UpdateInventoryMovementDto updateDTO)
        {
            try
            {
                var movement = await _movementRepository
            .GetByIdAsync(id);

                if (movement is null)
                {
                    return new ApiResponse<InventoryMovementDto>(
                        404,
                        "Inventory movement not found.");
                }

                var lastMovement = await _movementRepository
            .GetLastByProductIdAsync(movement.ProductId);

                if (lastMovement is null ||
                    lastMovement.Id != movement.Id)
                {
                    return new ApiResponse<InventoryMovementDto>(
                        400,
                        "Only the last inventory movement of a product can be updated.");
                }

                var product = await _productRepository
            .GetByIdAsync(movement.ProductId);

                if (product is null)
                {
                    return new ApiResponse<InventoryMovementDto>(
                        404,
                        "Product not found.");
                }

                if (!product.Status.Equals(ProductStatus.Active))
                {
                    return new ApiResponse<InventoryMovementDto>(
                        400,
                        "Product is inactive.");
                }

 
                product.Stock = movement.PreviousStock;

                var previousStock = product.Stock;

                ApplyMovement(
                    product,
                    updateDTO.Type,
                    updateDTO.Quantity);

                movement.Type = updateDTO.Type;
                movement.Quantity = updateDTO.Quantity;
                movement.Reason = updateDTO.Reason;
                movement.PreviousStock = previousStock;
                movement.ResultingStock = product.Stock;

                _movementRepository.Update(movement);

                _productRepository.Update(product);

                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<InventoryMovementDto>(
                    movement);

                return new ApiResponse<InventoryMovementDto>(
                    200,
                    response);

            }
            catch (InvalidOperationException ex)
            {
                return new ApiResponse<InventoryMovementDto>(
                    400,
                    ex.Message);
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryMovementDto>(
                    500,
                    $"An error occurred while updating the inventory movement: {ex.Message}");
            }
        }
        public async Task<ApiResponse<List<InventoryMovementDto>>> GetAllAsync()
        {
            try
            {
                var movements = await _movementRepository.GetAllWithIncludes(
                    new List<string> {
                        nameof(InventoryMovement.Product),
                        nameof(InventoryMovement.User)
                    });

                var response = _mapper.Map<List<InventoryMovementDto>>(movements); 
                
                return new ApiResponse<List<InventoryMovementDto>>(200, 
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<InventoryMovementDto>>(500,
                     $"An error occurred while retrieving inventory movements: {ex.Message}");
            }
        }

        public async Task<ApiResponse<InventoryMovementDto?>> GetByIdAsync(int id)
        {
            try
            {
                var movement = await _movementRepository.GetByIdAsync(id);

                if(movement is null)
                {
                    return new ApiResponse<InventoryMovementDto?>(404, 
                        "Inventory movement not found.");
                }

                var response = _mapper.Map<InventoryMovementDto>(movement);

                return new ApiResponse<InventoryMovementDto?>(200, 
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<InventoryMovementDto?>(500,
                    $"An error occurred while retrieving the inventory movement: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<InventoryMovementDto>>> GetByProductIdAsync(int productId)
        {
            try 
            { 
                var product = await _productRepository.GetByIdAsync(productId);

                if(product is null)
                {
                    return new ApiResponse<List<InventoryMovementDto>>(
                        404,
                        "Product not found.");
                }

                var movements = await _movementRepository.GetInventoryByProductIdAsync(productId);

                var response = _mapper.Map<List<InventoryMovementDto>>(movements); 

                return new ApiResponse<List<InventoryMovementDto>>(
                    200
                    , response); 
            }
            catch (Exception ex)
            { 
                return new ApiResponse<List<InventoryMovementDto>>(500, $"An error occurred while retrieving product movements: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<InventoryMovementDto>>> GetByUserIdAsync(int userId)
        {
            try
            {
                var user = await _userRepository
                    .GetByIdAsync(userId);

                if (user is null)
                {
                    return new ApiResponse<List<InventoryMovementDto>>(
                        404,
                        "User not found.");
                }

                var movements = await _movementRepository
                    .GetInventoryByUserIdAsync(userId);

                var response = _mapper.Map<List<InventoryMovementDto>>(
                    movements);

                return new ApiResponse<List<InventoryMovementDto>>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<InventoryMovementDto>>(
                    500,
                    $"An error occurred while retrieving user movements: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var movement = await _movementRepository.GetByIdAsync(id);
                if (movement == null)
                {
                    return new ApiResponse<bool>(404,
                        "Inventory movement not found.");
                }

                var lastMovement = await _movementRepository
                    .GetLastByProductIdAsync(movement.ProductId);

                if(lastMovement is null || lastMovement.Id != movement.Id)
                {
                    return new ApiResponse<bool>(400,
                        "Only the last inventory movement can be deleted.");
                }

                var product = await _productRepository.GetByIdAsync(movement.ProductId); 

                if (product is null) 
                { 
                    return new ApiResponse<bool>(404, 
                        "Product not found."); 
                }
                // Revertir el efecto del movimiento. 
                product.Stock = movement.PreviousStock; 
                var deleted = await _movementRepository.DeleteAsync(id); 

                if (!deleted) 
                { 
                    return new ApiResponse<bool>(404,
                        "Inventory movement could not be deleted."); 
                } 
                _productRepository.Update(product); 

                await _unitOfWork.SaveChangesAsync(); 

                return new ApiResponse<bool>(200,
                    true);

            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(500,
                    $"An error occurred while deleting the inventory movement: {ex.Message}");
            }
        }

        private void ApplyMovement( Product product,
            InventoryMovementType type,
            int quantity)
        {
                    switch (type)
                    {
                        case InventoryMovementType.Entry:

                            product.Stock += quantity;

                            break;

                        case InventoryMovementType.Exit:

                            if (product.Stock < quantity)
                            {
                                throw new InvalidOperationException(
                                    "Insufficient stock.");
                            }

                            product.Stock -= quantity;

                            break;

                        case InventoryMovementType.Adjustment:

                            product.Stock = quantity;

                            break;

                        default:

                            throw new InvalidOperationException(
                                "Invalid inventory movement type.");

                    }


        }

    
        private async Task SaveMovementAsync(
            InventoryMovement movement,
            Product product)
                {
                    await _movementRepository.AddAsync(movement);

                    _productRepository.Update(product);

                    await _unitOfWork.SaveChangesAsync();
                }

    }
}