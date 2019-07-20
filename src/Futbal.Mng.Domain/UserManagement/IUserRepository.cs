using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Futbal.Mng.Domain.UserManagement
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task<User> GetUser(string email);

        Task<User> GetUser(Guid id);

        Task<IEnumerable<User>> GetUsers();

        Task UpdateAsync(User user);

        Task Remove(User user);
    }
}