using System;
using System.Collections.Generic;

namespace Futbal.Mng.Infrastructure.DTO
{
    public class GameDetailsDto
    {
        public Guid Id { get; set; }

        public OwnerDto Owner { get; set; }

        public string Name { get; set; }

        public DateTime GameDate { get; set; }

        public PlaceDto Address { get; set; }
        public IEnumerable<AttendeeDto> Attendees { get; set; }
    }
}