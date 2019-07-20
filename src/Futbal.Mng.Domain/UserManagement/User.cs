using System;
using System.Collections.Generic;
using System.Linq;
using Futbal.Mng.Domain.Event;

namespace Futbal.Mng.Domain.UserManagement
{
    public class User
    {
        public Guid Id { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string Email { get; set; }

        public bool IsAvailable { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public DateTime? UpdatedOn { get; private set; }

        public ICollection<UserGame> Games { get; private set; }

        private User() {}

        public User(string username, string password, string email)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Email = email;
            CreatedOn = DateTime.UtcNow;
        }

        public void SetAvailability(Guid userId, Guid gameId, bool isAvailable)
        {
            var userGame = Games.FirstOrDefault(g => g.GameId == gameId && g.UserId == userId);
            userGame.SetUserAvailability(isAvailable);
            
            UpdatedOn = DateTime.UtcNow;
        }
    }
}