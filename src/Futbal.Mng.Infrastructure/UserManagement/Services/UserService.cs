using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public UserService(IUserRepository userRepository,
        IGameRepository gameRepository)
        {
            _userRepository = userRepository;
            _gameRepository = gameRepository;
        }
        public async Task<GameDetailsDto> GetAsync(Guid id)
        {
           var game = await _userRepository.GetUser(id);

           return new GameDetailsDto{ };
        }
    }
}