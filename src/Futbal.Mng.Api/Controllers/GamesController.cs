using System;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.Commands;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;
using Microsoft.AspNetCore.Mvc;

namespace Futbal.Mng.Api.Controllers
{
    
    public class GamesController : ApiBaseController
    {
        private readonly IGameService _gameService;
        private readonly ICommandBus _commandBus;

        public GamesController(ICommandBus commandBus)
        {
            
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<string> ServiceEndpoint()
        {
            return "games-management-service";
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGame(CreateNewGameCommand command)
        {
            await _commandBus.SendCommandAsync(command);
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
        public async Task<IActionResult> UpdateGamePlace(Guid id, ChangeGamePlaceCommand command)
        {
            command.GameId = id;
            await _commandBus.SendCommandAsync(command);
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