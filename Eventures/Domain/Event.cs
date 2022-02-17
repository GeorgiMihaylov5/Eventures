using System;

namespace Eventures.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TotalTickets { get; set; }
        public double PricePerTicket { get; set; }
        public EventUser Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
