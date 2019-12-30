
using System.Threading.Tasks;
using Futbal.Mng.Webapi;

namespace Futbal.Mng.Api.Services
{
    public class GameResponse : GameService.GameServiceBase
    {
        public override Task<GameReply> Game(Webapi.GameResponse request, Grpc.Core.ServerCallContext context)
        {
                var response = new GameReply();

                return Task.FromResult<GameReply>(response);
        }
    }
}