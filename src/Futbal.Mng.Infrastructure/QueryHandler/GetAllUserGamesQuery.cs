using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;

namespace Futbal.Mng.Infrastructure.QueryHandler
{
    public class GetAllUserGamesQuery: IQuery<IList<UserGamesListDto>>
    {
        public Guid UserId { get; }

        public GetAllUserGamesQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    internal class GetAllUserGamesHandler : IHandleQuery<GetAllUserGamesQuery, IList<UserGamesListDto>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetAllUserGamesHandler(IGameRepository gameRepository,
        IUserRepository userRepository,
        IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public async Task<IList<UserGamesListDto>> HandleAsync(GetAllUserGamesQuery query)
        {
            var userGames = await _gameRepository.GetUserGames(query.UserId);

            if(userGames == null)
                return new List<UserGamesListDto>();

            var mappedGames = _mapper.Map<IList<UserGamesListDto>>(userGames);

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