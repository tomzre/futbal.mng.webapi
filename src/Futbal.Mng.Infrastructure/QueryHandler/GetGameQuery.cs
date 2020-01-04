using System;
using Futbal.Mng.Infrastructure.DTO;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;

namespace Futbal.Mng.Infrastructure.QueryHandler
{
    public class GetGameQuery : IQuery<GameDetailsDto>
    {
        public Guid Id { get; }

        public GetGameQuery(Guid id)
        {
            Id = id;
        }
    }
}