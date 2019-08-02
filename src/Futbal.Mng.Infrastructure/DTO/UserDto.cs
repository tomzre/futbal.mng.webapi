using System;

namespace Futbal.Mng.Infrastructure.DTO
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAvailable { get; set; }
    }
}