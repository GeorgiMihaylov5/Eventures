using Eventures.Data;
using Eventures.Domain;
using Eventures.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext context;
        public OrdersController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(OrderCreateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = context.Users.SingleOrDefault(u => u.Id == userId);
                var ev = context.Events.SingleOrDefault(e => e.Id == model.EventId);

                if (user == null || ev == null || ev.TotalTickets < model.TIcketsCount)
                {
                    return RedirectToAction("All", "Events");
                }
                Order orderForDb = new Order
                {
                    OrderedOn = DateTime.UtcNow,
                    EventId = model.EventId,
                    TicketsCount = model.TIcketsCount,
                    CustomerId = userId
                };

                ev.TotalTickets -= model.TIcketsCount;

                context.Events.Update(ev);
                context.Orders.Add(orderForDb);
                context.SaveChanges();
            }
            return RedirectToAction("All", "Events");
        }
    }
}
