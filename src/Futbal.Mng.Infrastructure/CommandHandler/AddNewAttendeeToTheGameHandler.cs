using System;
using System.Threading.Tasks;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.Commands;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.CommandHandler
{
    public class AddNewAttendeeToTheGameHandler : IHandleCommand<AddAttendeeToTheGameCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;

        public AddNewAttendeeToTheGameHandler(IGameRepository gameRepository, IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }
        public async Task HandleAsync(AddAttendeeToTheGameCommand command)
        {
            var game = await _gameRepository.GetGame(command.GameId);
            if(game == null)
            {
                throw new ArgumentException($"Cannot find game with given id: {command.GameId}");
            }
            
            var user = await _userRepository.GetUser(command.NewAttendeeId);
            if(user == null)
            {
                throw new ArgumentException($"Cannot add not existing attendee with id: {command.NewAttendeeId} to the game: {game.Name}, requested game id: {command.GameId}");
            }
            
            game.AddAttendee(user);

            await _gameRepository.UpdateAsync(game);
        }
    }
}