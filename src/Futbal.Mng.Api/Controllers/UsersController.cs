using System;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Futbal.Mng.Api.Controllers
{
    public class UsersController : ApiBaseController
    {
        private readonly IUserService _userService;
        private readonly IGameService _gameService;

        public UsersController(IUserService userService,
        IGameService gameService)
        {
            _userService = userService;
            _gameService = gameService;
        }

        [HttpGet("{userId}/mygames")]
        public async Task<IActionResult> GetMyGames(Guid userId)
        {
            var games = await _gameService.GetUserGames(userId);

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