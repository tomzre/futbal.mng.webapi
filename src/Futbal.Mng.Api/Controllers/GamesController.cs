using System;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Futbal.Mng.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<string> ServiceEndpoint()
        {
            return "games-management-service";
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGame(GameDto newGame)
        {
            await _gameService.AddNewGame(newGame);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(Guid id)
        {
            var game = await _gameService.GetAsync(id);
            return Ok(game);
        }

        [HttpPut("{id}/attendees/{newAttendeeId}")]
        public async Task<IActionResult> AddAttendeeToGame(Guid id, Guid newAttendeeId)
        {
            await _gameService.AddAttendee(id, newAttendeeId);

            return Ok();
        }

        [HttpPut("{id}/places")]
        public async Task<IActionResult> UpdateGamePlace(Guid id, PlaceDto newAddress)
        {
            await _gameService.UpdateGamePlace(id, newAddress);
            return Ok();
        }

        [HttpPut("{id}/attendees/{attendeeId}/available")]
        public async Task<IActionResult> UpdateAttendeeAvailability(Guid id, SetAvailabilityDto availability)
        {
            await _gameService.SetAttendeeAvailability(id, availability);
            return Ok();
        }
    }
}