using Eventures.Domain;
using System;
using System.Collections.Generic;

namespace Eventures.Abstraction
{
    public interface IEventService
    {
        bool Create(string name, string place, DateTime start, DateTime end, int totalTickets, double pricePerTicket);
        bool Update(int id, string name, string place, DateTime start, DateTime end, int totalTickets, double pricePerTicket);
        List<Event> GetEvents();
        Event GetEventById(int id);
        bool RemoveById(int id);
    }
}
