using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventures.Domain
{
    public class Eventure
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TotalTickets { get; set; }
        public double PricePerTicket { get; set; }
        public EventuresUser Owner { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
