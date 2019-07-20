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

        public async Task AddAttendee(Guid gameId, Guid newAttendeeId)
        {
            var user = await _userRepository.GetUser(newAttendeeId);
            await _gameRepository.AddAttendee(gameId, user);
        }

        public async Task AddNewGame(GameDto newGame)
        {
            var owner = await _userRepository.GetUser(newGame.OwnerId);
            var game = new Game(newGame.Name, newGame.GameDate, owner);
            await _gameRepository.AddGame(game);
        }

        public async Task<GameDetailsDto> GetAsync(Guid id)
        {
            var game = await _gameRepository.GetAsync(id);

            var mappedGame = _mapper.Map<GameDetailsDto>(game);

            mappedGame.Attendees = game.Attendees.Select(x =>  
            new AttendeeDto{ 
                IsAvailable = x.IsAvailable,
                LastName = x.User?.Username,
                FirstName = x.User?.Email,
                Id = x.UserId
                });
                
            return mappedGame;
        }

        public async Task UpdateGamePlace(Guid id, PlaceDto newAddress)
        {
            await _gameRepository.UpdatePlace(id, Address.Create(newAddress.Street, newAddress.Number));
        }

        public async Task<IEnumerable<GameDetailsDto>> GetUserGames(Guid userId)
        {
            var userGames = await _gameRepository.GetUserGames(userId);

            var mappedGames = _mapper.Map<IList<GameDetailsDto>>(userGames);

            foreach (var mappedGame in mappedGames)
            {
                mappedGame.Attendees = 
                userGames
                .Where(x => x.Id == mappedGame.Id)
                .SelectMany(x => x.Attendees
                    .Select(y => new AttendeeDto
                        {
                            IsAvailable = y.IsAvailable,
                            LastName = y.User?.Username,
                            FirstName = y.User?.Email,
                            Id = y.UserId
                        }));
            }

            return mappedGames;
        }

        public async Task SetAttendeeAvailability(Guid id, SetAvailabilityDto availability)
        {
            var user = await _userRepository.GetUser(availability.UserId);
            user.SetAvailability(availability.UserId, id,  availability.IsAvailable);

            await _userRepository.UpdateAsync(user);
        }
    }
}