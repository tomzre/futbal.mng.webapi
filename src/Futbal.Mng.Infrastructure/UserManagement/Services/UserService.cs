using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;

namespace Futbal.Mng.Infrastructure.UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
        IGameRepository gameRepository,
        IMapper mapper)
        {
            _userRepository = userRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        public async Task<GameDetailsDto> GetAsync(Guid id)
        {
           var game = await _userRepository.GetUser(id);

           return new GameDetailsDto{ };
        }

        public async Task CreateUser(UserDto userDto)
        {
            var user = new User(userDto.Username, userDto.Password, userDto.Email);
            await _userRepository.Add(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();

            return users.Select(user => 
            new UserDto
                {
                    Id = user.Id,
                    Username = user.Username
                });
        }
    }
}