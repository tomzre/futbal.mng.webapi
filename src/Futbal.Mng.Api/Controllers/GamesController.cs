using System;
using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.Commands;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;
using Futbal.Mng.Infrastructure.QueryHandler;
using Microsoft.AspNetCore.Mvc;

namespace Futbal.Mng.Api.Controllers
{
    
    public class GamesController : ApiBaseController
    {
        private readonly ICommandBus _commandBus;
        
        private readonly IQueryBus _queryBus;

        public GamesController(ICommandBus commandBus, IQueryBus queryBus)
        {
            
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        [HttpGet]
        public string ServiceEndpoint()
        {
            return "games-management-service-test";
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
            var query = new GetGameQuery(id);
            
            var game = await _queryBus.DispatchAsync<GetGameQuery, GameDetailsDto>(query);
            
            return Ok(game);
        }

        [HttpPut("{id}/attendees")]
        public async Task<IActionResult> AddAttendeeToGame(Guid id, AddAttendeeToTheGameCommand cmd)
        {
            cmd.GameId = id;
            await _commandBus.SendCommandAsync(cmd);

            return Ok();
        }

        [HttpPut("{id}/places")]
        public async Task<IActionResult> UpdateGamePlace(Guid id, ChangeGamePlaceCommand command)
        {
            command.GameId = id;
            await _commandBus.SendCommandAsync(command);
            return Ok();
        }

        [HttpPut("{id}/attendees/available")]
        public async Task<IActionResult> UpdateAttendeeAvailability(Guid id, SetAttendeeAvailabilityCommand cmd)
        {
            cmd.GameId = id;
            await _commandBus.SendCommandAsync(cmd);
            return Ok();
        }
    }
}