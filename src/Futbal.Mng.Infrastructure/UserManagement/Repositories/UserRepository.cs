using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Futbal.Mng.Infrastructure.UserManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FutbalMngContext _context;

        public UserRepository(FutbalMngContext context)
        {
            _context = context;

        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            var isSaved = await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _context.Users
                .Include(x => x.Games)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Remove(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}