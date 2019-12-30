using System.Threading.Tasks;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.ValueObjects;
using Futbal.Mng.Infrastructure.Commands;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.CommandHandler
{
    public class ChangeGamePlaceHandler : IHandleCommand<ChangeGamePlaceCommand>
    {
        private readonly IGameRepository _gameRepository;
        public ChangeGamePlaceHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;

        }
        public void Handle(ChangeGamePlaceCommand command)
        {
            throw new System.NotImplementedException();
        }

        public async Task HandleAsync(ChangeGamePlaceCommand command)
        {
            await _gameRepository.UpdatePlace(command.GameId, Address.Create(command.Street, command.Number));
        }
    }
}