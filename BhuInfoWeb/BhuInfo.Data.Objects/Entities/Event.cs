using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Objects.Entities
{
    public class Event
    {
        public long EventId { get; set; }

        public string EventName { get; set; }

        public string Venue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Organizer { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
