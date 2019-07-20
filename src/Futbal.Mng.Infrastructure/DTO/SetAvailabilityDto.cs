using System;

namespace Futbal.Mng.Infrastructure.DTO
{
    public class SetAvailabilityDto
    {
        public Guid UserId { get; set; }
        public bool IsAvailable { get; set; }
    }
}