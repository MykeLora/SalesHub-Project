using SalesHub.Application.DTOs.Customer;
using SalesHub.Application.Wrappers;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface ICustomerService : IGenericService<CreateCustomerDto,UpdateCustomerDto,Customer,CustomerDto>
    {

    }
}

