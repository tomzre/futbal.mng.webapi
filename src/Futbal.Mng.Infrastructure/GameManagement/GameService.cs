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
            if(newGame.OwnerId == Guid.Empty)
            {
                throw new ArgumentException("Owner id is empty. Cannot create game");
            }
            var owner = await _userRepository.GetUser(newGame.OwnerId);

            if(owner == null)
            {
                throw new ArgumentNullException($"Cannot find user for given id: {newGame.OwnerId}");
            }
            Address address = null;
            if(newGame.Address != null)
                address = Address.Create(newGame.Address.Street, newGame.Address.Number);
            var game = new Game(newGame.Name, newGame.GameDate, owner);
            game.UpdatePlace(address);
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

        public async Task<IEnumerable<GameDetailsGridDto>> GetUserGames(Guid userId)
        {
            var userGames = await _gameRepository.GetUserGames(userId);

            if(userGames == null)
                return new List<GameDetailsGridDto>();

            var mappedGames = _mapper.Map<IList<GameDetailsGridDto>>(userGames);

            foreach (var mappedGame in mappedGames)
            {
                mappedGame.AvailableAttendees = userGames.FirstOrDefault(x => x.Id == mappedGame.Id).Attendees.Where(x => x.IsAvailable == true).Count();
                mappedGame.RequiredAttendees = 14;
                mappedGame.TotalAttendees = userGames.FirstOrDefault(x => x.Id == mappedGame.Id).Attendees.Count();
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