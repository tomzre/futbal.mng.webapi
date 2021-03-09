using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;
using Futbal.Mng.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Mvc;

namespace Futbal.Mng.Api.Controllers
{
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly IQueryBus _queryBus;

        public UsersController(IUserService userService,
        IQueryBus queryBus)
        {
            _userService = userService;
            _queryBus = queryBus;
        }

        [HttpGet("{userId}/games")]
        public async Task<IActionResult> GetMyGames(Guid userId)
        {
            var query = new GetAllUserGamesQuery(userId);
            var games = await _queryBus.DispatchAsync<GetAllUserGamesQuery, IEnumerable<UserGamesListDto>>(query);

            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            await _userService.CreateUser(userDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
    }
}