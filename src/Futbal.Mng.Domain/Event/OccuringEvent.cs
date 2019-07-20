using System;
using System.Collections.Generic;
using Futbal.Mng.Domain.UserManagement;

namespace Futbal.Mng.Domain.Event
{
    public class OccuringEvent
    {
        public Guid Id { get; private set; }

        public Game Game { get; private set; }

        public DateTime CreatedOn { get; private set; }

        public DateTime? UpdatedOn { get; private set; }
        private OccuringEvent()
        {
        }

        private void SetUpdatedOn()
        {
            UpdatedOn = DateTime.UtcNow;
        }
    }
}