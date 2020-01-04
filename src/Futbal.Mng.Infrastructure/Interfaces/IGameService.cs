using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;

namespace Futbal.Mng.Infrastructure.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDetailsGridDto>> GetUserGames(Guid userId);

        Task SetAttendeeAvailability(Guid id, SetAvailabilityDto availability);
    }
}