using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;

namespace Futbal.Mng.Infrastructure.QueryHandler
{
    public class GetGameQueryHandler : IHandleQuery<GetGameQuery, GameDetailsDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGameQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<GameDetailsDto> HandleAsync(GetGameQuery query)
        {
            var game = await _gameRepository.GetAsync(query.Id);

            var mappedGame = _mapper.Map<GameDetailsDto>(game);

            mappedGame.Attendees = game.Attendees.Select(x =>
            new AttendeeDto
            {
                IsAvailable = x.IsAvailable,
                LastName = x.User?.Username,
                FirstName = x.User?.Email,
                Id = x.UserId
            });

            return mappedGame;
        }
    }
}