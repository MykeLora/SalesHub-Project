using AutoMapper;
using SalesHub.Application.DTOs.Sale;
using SalesHub.Application.DTOs.Sale.SaleDetail;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Interface.Services.Sale;
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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfwork _unitOfWork;

        private readonly IDiscountCalculator _discountCalculator;
        private readonly ITaxCalculator _taxCalculator;
        private readonly ISaleNumberGenerator _saleNumberGenerator;

        public SaleService(
            ISaleRepository saleRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfwork unitOfWork,
            IDiscountCalculator discountCalculator,
            ITaxCalculator taxCalculator,
            ISaleNumberGenerator saleNumberGenerator)
        {
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            _discountCalculator = discountCalculator;
            _taxCalculator = taxCalculator;
            _saleNumberGenerator = saleNumberGenerator;
        }

        public async Task<ApiResponse<SaleDto>> CreateAsync(CreateSaleDto dto)
        {
            try
            {
                var customer = await _customerRepository
                    .GetByIdAsync(dto.CustomerId);

                if (customer is null)
                {
                    return new ApiResponse<SaleDto>(
                        404,
                        "Customer not found.");
                }

                if (!customer.IsActive)
                {
                    return new ApiResponse<SaleDto>(
                        400,
                        "Customer is inactive.");
                }

                var (details, subtotal) =
                    await BuildSaleDetailsAsync(dto.Details);

                var saleNumberG = _saleNumberGenerator.GenerateAsync();

                var sale = new Sale
                {
                    SaleNumber = saleNumberG,
                    SaleDate = DateTime.UtcNow,
                    CustomerId = dto.CustomerId,
                    UserId = 2,
                    DiscountPercentage = dto.DiscountPercentage,
                    SubTotal = subtotal,
                    Details = details
                };

                sale.Discount = _discountCalculator.CalculateDiscount(
                    sale.SubTotal,
                    sale.DiscountPercentage);

                var taxableAmount =
                    sale.SubTotal - sale.Discount;

                sale.Tax = _taxCalculator.Calculate(
                    taxableAmount);

                sale.Total =
                    taxableAmount + sale.Tax;

                await _saleRepository.AddAsync(sale);

                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<SaleDto>(sale);

                return new ApiResponse<SaleDto>(
                    201,
                    response);
            }
            catch (KeyNotFoundException ex)
            {
                return new ApiResponse<SaleDto>(
                    404,
                    ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return new ApiResponse<SaleDto>(
                    400,
                    ex.Message);
            }
            catch (Exception ex)
            {
                return new ApiResponse<SaleDto>(
                    500,
                    $"An error occurred while creating the sale: {ex.Message}");
            }
        }


        public async Task<ApiResponse<List<SaleDto>>> GetAllAsync()
        {
            try
            {
                var sales = await _saleRepository
                    .GetAllWithDetailsAsync();

                var response = _mapper.Map<List<SaleDto>>(sales);

                return new ApiResponse<List<SaleDto>>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SaleDto>>(
                    500,
                    $"An error occurred while retrieving sales: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<SaleDto>>> GetByCustomerIdAsync(
            int customerId)
        {
            try
            {
                var sales = await _saleRepository
                    .GetByCustomerIdAsync(customerId);

                var response = _mapper.Map<List<SaleDto>>(sales);

                return new ApiResponse<List<SaleDto>>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SaleDto>>(
                    500,
                    $"An error occurred while retrieving customer sales: {ex.Message}");
            }
        }

        public async Task<ApiResponse<SaleDto>> GetByIdAsync(int id)
        {
            try
            {
                var sale = await _saleRepository
                    .GetWithDetailsAsync(id);

                if (sale is null)
                {
                    return new ApiResponse<SaleDto>(
                        404,
                        "Sale not found.");
                }

                var response = _mapper.Map<SaleDto>(sale);

                return new ApiResponse<SaleDto>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<SaleDto>(
                    500,
                    $"An error occurred while retrieving the sale: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<SaleDto>>> GetByUserIdAsync(
            int userId)
        {
            try
            {
                var sales = await _saleRepository
                    .GetByUserIdAsync(userId);

                var response = _mapper.Map<List<SaleDto>>(sales);

                return new ApiResponse<List<SaleDto>>(
                    200,
                    response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SaleDto>>(
                    500,
                    $"An error occurred while retrieving user sales: {ex.Message}");
            }
        }


        public async Task<ApiResponse<SaleDto>> UpdateAsync(
            int id,
            UpdateSaleDto updateDTO)
        {
            try
            {
                
                var sale = await _saleRepository.GetWithDetailsAsync(id);

                if (sale is null)
                {
                    return new ApiResponse<SaleDto>(
                        404,
                        "Sale not found.");
                }

                
                var customer = await _customerRepository
                    .GetByIdAsync(updateDTO.CustomerId);

                if (customer is null)
                {
                    return new ApiResponse<SaleDto>(
                        404,
                        "Customer not found.");
                }

                if (!customer.IsActive)
                {
                    return new ApiResponse<SaleDto>(
                        400,
                        "Customer is inactive.");
                }

                var newDetails = await BuildUpdatedSaleDetailsAsync(
                    sale.Id,
                    updateDTO.Details);

                var subtotal = newDetails.Sum(d => d.SubTotal);

                var discount = _discountCalculator.CalculateDiscount(
                    subtotal,
                    updateDTO.DiscountPercentage);

                var taxableAmount = subtotal - discount;

                var tax = _taxCalculator.Calculate(taxableAmount);

                var total = taxableAmount + tax;

                sale.CustomerId = updateDTO.CustomerId;
                sale.DiscountPercentage = updateDTO.DiscountPercentage;
                sale.SubTotal = subtotal;
                sale.Discount = discount;
                sale.Tax = tax;
                sale.Total = total;

                sale.Details.Clear();

                foreach (var detail in newDetails)
                {
                    sale.Details.Add(detail);
                }

                _saleRepository.Update(sale);

                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<SaleDto>(sale);

                return new ApiResponse<SaleDto>(
                    200,
                    response);
            }
            catch (KeyNotFoundException ex)
            {
                return new ApiResponse<SaleDto>(
                    404,
                    ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return new ApiResponse<SaleDto>(
                    400,
                    ex.Message);
            }
            catch (Exception ex)
            {
                return new ApiResponse<SaleDto>(
                    500,
                    $"An error occurred while updating the sale: {ex.Message}");
            }
        }


        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var deleted = await _saleRepository.DeleteAsync(id);

                if (!deleted)
                {
                    return new ApiResponse<bool>(
                        404,
                        "Sale not found.");
                }

                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>(
                    200,
                    true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(
                    500,
                    $"An error occurred while deleting the sale: {ex.Message}");
            }
        }

        private async Task<(List<SaleDetail> Details, decimal Subtotal)> BuildSaleDetailsAsync(List<CreateSaleDetailDto> detailDtos)
        {
            var details = new List<SaleDetail>();
            decimal subtotal = 0m;

            foreach (var detailDto in detailDtos)
            {
                var product = await _productRepository
                    .GetByIdAsync(detailDto.ProductId);

                if (product is null)
                    throw new KeyNotFoundException(
                        $"Product with ID {detailDto.ProductId} not found.");

                if (product.Status != ProductStatus.Active)
                    throw new InvalidOperationException(
                        $"Product '{product.Name}' is inactive.");

                var detailSubtotal =
                    product.Price * detailDto.Quantity;

                details.Add(new SaleDetail
                {
                    ProductId = product.Id,
                    Quantity = detailDto.Quantity,
                    UnitPrice = product.Price,
                    SubTotal = detailSubtotal
                });

                subtotal += detailSubtotal;
            }

            return (details, subtotal);
        }

        private async Task<List<SaleDetail>> BuildUpdatedSaleDetailsAsync(
        int saleId,
        List<UpdateSaleDetailDto> details)
        {
            var saleDetails = new List<SaleDetail>();

            foreach (var detailDTO in details)
            {
                var product = await _productRepository
                    .GetByIdAsync(detailDTO.ProductId);

                if (product is null)
                {
                    throw new KeyNotFoundException(
                        $"Product with ID {detailDTO.ProductId} not found.");
                }

                if (product.Status != ProductStatus.Active)
                {
                    throw new InvalidOperationException(
                        $"Product '{product.Name}' is inactive.");
                }

                var subtotal = product.Price * detailDTO.Quantity;

                saleDetails.Add(new SaleDetail
                {
                    SaleId = saleId,
                    ProductId = product.Id,
                    Quantity = detailDTO.Quantity,
                    UnitPrice = product.Price,
                    SubTotal = subtotal
                });
            }

            return saleDetails;
        }
    }
}
