using System;

namespace Futbal.Mng.Infrastructure.DTO
{
    public class UserGamesListDto
    {
        public Guid Id { get; set; }

        public OwnerDto Owner { get; set; }

        public string Name { get; set; }

        public DateTime GameDate { get; set; }

        public PlaceDto Address { get; set; }
        
        public int AvailableAttendees { get; set; }

        public int TotalAttendees { get; set; }

        public int RequiredAttendees { get; set; }
    }
}