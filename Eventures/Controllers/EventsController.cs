using Eventures.Abstraction;
using Eventures.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService service;

        public EventsController(IEventService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View(service.GetEvents());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Event item)
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
