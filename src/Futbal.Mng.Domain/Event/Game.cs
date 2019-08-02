using System;
using System.Collections.Generic;
using System.Linq;
using Futbal.Mng.Domain.UserManagement;
using Futbal.Mng.Domain.ValueObjects;

namespace Futbal.Mng.Domain.Event
{
    public class Game
    {
        private ICollection<UserGame> _attendees = new HashSet<UserGame>();

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime GameDate { get; private set; }

        public User Owner { get; set; }

        public Address Place { get; private set; }

        public IEnumerable<UserGame> Attendees => _attendees;

        public DateTime CreatedOn { get; private set; }

        public DateTime? UpdatedOn { get; private set; }

        private Game(){}

        public Game(string name, DateTime gameDate, User owner)
        {
            Name = name;
            GameDate = gameDate;
            Owner = owner;
            CreatedOn = DateTime.UtcNow;
        }

        public void UpdateGameDate(DateTime gameDate)
        {
            GameDate = gameDate;
            SetUpdatedOn();
        }

        public bool UpdatePlace(Address newAddress)
        {
            if(Place != null)
            {
                if(newAddress.Equals(Place))
                    return false;
            }
            Place = newAddress;
            SetUpdatedOn();
            return true;
        }

        public void AddAttendee(User attendee)
        {
            var newAttendee = new UserGame(this, attendee);
            _attendees.Add(newAttendee);
            SetUpdatedOn();
        }

        public void RemoveAttendee(User attendee)
        {
            var removeAttendee = new UserGame(this, attendee);
            _attendees.Remove(removeAttendee);
            SetUpdatedOn();
        }

        private void SetUpdatedOn()
        {
            UpdatedOn = DateTime.UtcNow;
        }
    }
}