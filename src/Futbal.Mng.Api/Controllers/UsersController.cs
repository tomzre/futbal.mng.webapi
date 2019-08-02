using System;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Futbal.Mng.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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
        public async Task CreateUser(UserDto userDto)
        {
            await _userService.CreateUser(userDto);
        }
    }
}