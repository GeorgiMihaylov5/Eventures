using Eventures.Abstraction;
using Eventures.Data;
using Eventures.Domain;
using Eventures.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace Eventures.Controllers
{
    public class EventuresController : Controller
    {
        private readonly IEventuresService service;
        private readonly ApplicationDbContext context;

        public EventuresController(ApplicationDbContext _context, IEventuresService service)
        {
            this.service = service;
            this.context = _context;
        }
        public IActionResult Index(string searchString)
        {
            List<EventAllViewModel> events = context.Events
                .Select(x => new EventAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Place = x.Place,
                    Start = x.Start.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                    End = x.End.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                    Owner = x.Owner.UserName
                }).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(x => x.Place.Contains(searchString)).ToList();
            }
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Eventure item)
        {
            if (ModelState.IsValid)
            {
                var created = service.Create(item.Name, item.Place, item.Start, item.End, item.TotalTickets, item.PricePerTicket);

                if (created)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public IActionResult My(string searchString)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.Users.SingleOrDefault(u => u.Id == currentUserId);

            if (user == null)
            {
                return null;
            }

            List<OrderListingViewModel> orders = context
                .Orders
                .Where(x => x.CustomerId == user.Id)
                .Select(x => new OrderListingViewModel
                {
                    Id = x.Id,
                    EventId = x.EventureId,
                    EventName = x.Eventure.Name,
                    EventStart = x.Eventure.Start.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                    EventEnd = x.Eventure.End.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                    EventPlace = x.Eventure.Place,
                    OrderedOn = x.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                    CustomerId = x.CustomerId,
                    CustomerUsername = x.Customer.UserName,
                    TicketsCount = x.TicketsCount
                }).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(x => x.EventPlace.Contains(searchString)).ToList();
            }
            return View(orders);
        }
    }
}
