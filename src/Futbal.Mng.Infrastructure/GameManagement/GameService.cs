using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Domain.ValueObjects;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;

namespace Futbal.Mng.Infrastructure.GameManagement
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GameService(IGameRepository gameRepository,
        IUserRepository userRepository,
        IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        
        public async Task<IEnumerable<GameDetailsGridDto>> GetUserGames(Guid userId)
        {
            var userGames = await _gameRepository.GetUserGames(userId);

            if(userGames == null)
                return new List<GameDetailsGridDto>();

            var mappedGames = _mapper.Map<IList<GameDetailsGridDto>>(userGames);

            foreach (var mappedGame in mappedGames)
            {
                mappedGame.AvailableAttendees = userGames
                    .FirstOrDefault(x => x.Id == mappedGame.Id)
                    .Attendees.Where(x => x.IsAvailable == true)
                    .Count();

                mappedGame.RequiredAttendees = 14;
                mappedGame.TotalAttendees = userGames
                    .FirstOrDefault(x => x.Id == mappedGame.Id)
                    .Attendees
                    .Count();
            }

            return mappedGames;
        }
    }
}