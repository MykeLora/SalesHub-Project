using SalesHub.Application.DTOs.User;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Interface.Services
{
    public interface IUserService : IGenericService<CreateUserDto,UpdateUserDto,User,UserDto>
    {
    }
}