using System;
using Futbal.Mng.Domain.UserManagement;

namespace Futbal.Mng.Domain.Event
{
    public class UserGame
    {
        public Guid UserId { get; private set; }

        public Guid GameId { get; private set; }

        public Game Game { get; private set; }

        public User User { get; private set; }

        public bool IsAvailable {get; private set;}

        private UserGame() { }

        public UserGame(Game game, User user) =>
        (Game, User) = (game, user);
        
        public void SetUserAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}