using System;
using System.Threading.Tasks;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Domain.ValueObjects;
using Futbal.Mng.Infrastructure.Commands;
using Futbal.Mng.Infrastructure.Interfaces;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.CommandHandler
{
    public class CreateNewGameHandler : IHandleCommand<CreateNewGameCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public CreateNewGameHandler(IGameRepository gameRepository,
        IUserRepository userRepository)
        {
           _gameRepository = gameRepository;
            _userRepository = userRepository;
        }
        public void Handle(CreateNewGameCommand command)
        {
            throw new System.NotImplementedException();
        }

        public async Task HandleAsync(CreateNewGameCommand command)
        {
            if(command.OwnerId == Guid.Empty)
            {
                throw new ArgumentException("Owner id is empty. Cannot create game");
            }
            var owner = await _userRepository.GetUser(command.OwnerId);

            if(owner == null)
            {
                throw new ArgumentNullException($"Cannot find user for given id: {command.OwnerId}");
            }
            Address address = Address.Create(command.Street, command.Number);
            var game = new Game(command.Name, command.GameDate, owner);
            game.UpdatePlace(address);
            await _gameRepository.AddGame(game);
        }
    }
}