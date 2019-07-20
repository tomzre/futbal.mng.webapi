using System;
using System.Threading.Tasks;
using Futbal.Mng.Domain.UserManagement;

namespace Futbal.Mng.Domain.Event
{
    public interface IOccuringEventRepository
    {
         Task AddAttendee(Guid gameId, User attendee);
    }
}