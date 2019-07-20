using System;

namespace Futbal.Mng.Infrastructure.DTO
{
    public class AttendeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAvailable { get; set; }
    }
}