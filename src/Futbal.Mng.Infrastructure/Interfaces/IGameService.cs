using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;

namespace Futbal.Mng.Infrastructure.Interfaces
{
    public interface IGameService
    {
        Task AddNewGame(GameDto newGame);
        Task<GameDetailsDto> GetAsync(Guid id);

        Task AddAttendee(Guid gameId, Guid newAttendeeId);

        Task UpdateGamePlace(Guid id, PlaceDto newAddress);

        Task<IEnumerable<GameDetailsDto>> GetUserGames(Guid userId);

        Task SetAttendeeAvailability(Guid id, SetAvailabilityDto availability);
    }
}