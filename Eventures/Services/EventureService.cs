using Eventures.Abstraction;
using Eventures.Data;
using Eventures.Domain;
using Eventures.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eventures.Services
{
    public class EventureService : IEventuresService
    {
        private readonly ApplicationDbContext context;

        public EventureService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Create(string name, string place, DateTime start, DateTime end, int totalTickets, double pricePerTicket)
        {
            Domain.Eventure item = new Domain.Eventure { Name = name, Place = place, Start = start, End = end, TotalTickets = totalTickets, PricePerTicket = pricePerTicket };
            context.Events.Add(item);
            return context.SaveChanges() != 0;
        }

        public Domain.Eventure GetEventById(int id)
        {
            return context.Events.Find(id);
        }

        public List<Eventure> GetEvents(string searchString)
        {
            //List<EventAllViewModel> events = context.Events
            //    .Select(x => new Eventure
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Place = x.Place,
            //        Start = x.Start.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
            //        End = x.End.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
            //        Owner = x.Owner.UserName
            //    }).ToList();

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    events = events.Where(x => x.Place.Contains(searchString)).ToList();
            //}
            return null;
        }

        public bool RemoveById(int id)
        {
            var item = context.Events.Find(id);
            if (item == default(Domain.Eventure))
            {
                return false;
            }
            context.Events.Remove(item);
            return context.SaveChanges() != 0;
        }

        public bool Update(int id, string name, string place, DateTime start, DateTime end, int totalTickets, double pricePerTicket)
        {
            var item = context.Events.Find(id);
            if (item == default(Domain.Eventure))
            {
                return false;
            }
            item.Name = name;
            item.Place = place;
            item.Start = start;
            item.End = end; 
            item.TotalTickets = totalTickets;
            item.PricePerTicket = pricePerTicket;
            context.Events.Update(item);
            return context.SaveChanges() != 0;
        }
    }
}
