using System.Threading.Tasks;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Infrastructure.EF;
using Futbal.Mng.Domain.ValueObjects;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Futbal.Mng.Domain.UserManagement;
using System.Collections.Generic;
using System.Threading;

namespace Futbal.Mng.Infrastructure.GameManagement
{
    public class GameRepository : IGameRepository
    {
        private readonly FutbalMngContext _context;

        public GameRepository(FutbalMngContext context)
        {
            _context = context;
        }
        public async Task AddGame(Game newGame)
        {
            _context.Games.Add(newGame);

            await _context.SaveChangesAsync();
        }

        public async Task<Game> GetGame(Guid id)
        {
            return await _context.Games.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Game> GetAsync(Guid id)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(1000);
            return await _context.Games
                .Include(x => x.Owner)
                .Include(x => x.Attendees)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationTokenSource.Token);
        }

        public async Task<ICollection<Game>> GetUserGames(Guid userId)
        {
            return await _context.Games
                .Include(x => x.Owner)
                .Include(x => x.Attendees)
                .ThenInclude(x => x.User)
                .Where(x => x.Owner.Id == userId || x.Attendees.Any(y => y.UserId == userId))
                .ToListAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            await _context.SaveChangesAsync();
        }
    }
}