using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Domain.ValueObjects;

namespace Futbal.Mng.Domain.Event
{
    public interface IGameRepository
    {
         Task<Game> GetAsync(Guid id);

         Task AddGame(Game newGame);
        
         Task UpdateAsync(Game game);

         Task<Game> GetGame(Guid id);

         Task<ICollection<Game>> GetUserGames(Guid userId);
    }
}