using AutoMapper;
using SalesHub.Application.DTOs.Customer;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Services
{
    public class CustomerService : GenericService<CreateCustomerDto, UpdateCustomerDto, Customer, CustomerDto>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IUnitOfwork unitOfWork) 
            : base(customerRepository, mapper, unitOfWork)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }

      
    }
}