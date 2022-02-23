using Eventures.Abstraction;
using Eventures.Data;
using Eventures.Domain;
using Eventures.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .Select(x => new Eventure
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
    }
}
