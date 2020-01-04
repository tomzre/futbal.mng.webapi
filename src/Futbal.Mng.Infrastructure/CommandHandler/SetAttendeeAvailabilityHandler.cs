using System;
using System.Threading.Tasks;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.Commands;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.CommandHandler
{
    public class SetAttendeeAvailabilityHandler : IHandleCommand<SetAttendeeAvailabilityCommand>
    {
        private readonly IUserRepository _userRepository;

        public SetAttendeeAvailabilityHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(SetAttendeeAvailabilityCommand command)
        {
            var user = await _userRepository.GetUser(command.UserId);
            if(user == null)
            {
                throw new ArgumentException($"Cannot find user with id:{command.UserId}");
            }

            user.SetAvailability(command.UserId, command.GameId,  command.IsAvailable);

            await _userRepository.UpdateAsync(user);
        }
    }
}