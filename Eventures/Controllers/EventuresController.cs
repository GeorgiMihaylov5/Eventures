using Eventures.Abstraction;
using Eventures.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class EventuresController : Controller
    {
        private readonly IEventuresService service;

        public EventuresController(IEventuresService service)
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
