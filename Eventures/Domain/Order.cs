using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Domain
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public string EventId { get; set; }
        public Eventure Eventure { get; set; }
        public string CustomerId { get; set; }
        public EventuresUser Customer { get; set; }
        public int TicketsCount { get; set; }
    }
}
