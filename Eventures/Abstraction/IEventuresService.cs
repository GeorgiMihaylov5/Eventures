using System;
using System.Collections.Generic;
using Eventures.Domain;

namespace Eventures.Abstraction
{
    public interface IEventuresService
    {
        bool Create(string name, string place, DateTime start, DateTime end, int totalTickets, double pricePerTicket);
        bool Update(int id, string name, string place, DateTime start, DateTime end, int totalTickets, double pricePerTicket);
        List<Eventure> GetEvents(string searchString);
        Eventure GetEventById(int id);
        bool RemoveById(int id);
    }
}
