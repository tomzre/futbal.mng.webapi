using System;
using System.Collections.Generic;

namespace Futbal.Mng.Domain.Event
{
    public class GameOwner
    {
        public Guid UserId { get; private set; }

        public IEnumerable<Game> Games { get; private set; }
    }
}