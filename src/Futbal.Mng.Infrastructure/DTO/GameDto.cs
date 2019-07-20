using System;

namespace Futbal.Mng.Infrastructure.DTO
{
    public class GameDto
    {
        public string Name { get; set; }

        public DateTime GameDate { get; set; }

        public Guid OwnerId { get; set; }
    }
}