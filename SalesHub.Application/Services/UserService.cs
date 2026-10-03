using AutoMapper;
using SalesHub.Application.DTOs.User;
using SalesHub.Application.Interface;
using SalesHub.Application.Interface.Repositories;
using SalesHub.Application.Interface.Services;
using SalesHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesHub.Application.Services
{
    public class UserService : GenericService<CreateUserDto, UpdateUserDto, User, UserDto>,IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfwork unitOfWork)
            : base(userRepository, mapper, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
    }
}
