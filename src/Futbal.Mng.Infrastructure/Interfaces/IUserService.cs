using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;

namespace Futbal.Mng.Infrastructure.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();

         Task<GameDetailsDto> GetAsync(Guid id);

         Task CreateUser(UserDto userDto);
    }
}