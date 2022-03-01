using Eventures.Data;
using Eventures.Domain;
using Eventures.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eventures.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext context;
        public OrdersController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public IActionResult My()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.Users.SingleOrDefault(u => u.Id == userId);

            List<OrderListingViewModel> orders = context
                 .Orders
                 .Select(x => new OrderListingViewModel
                 {
                     EventName = x.Eventure.Name,
                     OrderedOn = x.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                     CustomerUsername = x.Customer.UserName,
                     TicketsCount = x.TicketsCount
                 }).Where(x => x.CustomerUsername == user.UserName).ToList();

            return View("Index", orders);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = context.Users.SingleOrDefault(u => u.Id == userId);

            List<OrderListingViewModel> orders = context
                 .Orders
                 .Select(x => new OrderListingViewModel
                 {
                     EventName = x.Eventure.Name,
                     OrderedOn = x.OrderedOn.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
                     CustomerUsername = x.Customer.UserName,
                     TicketsCount = x.TicketsCount
                 }).ToList();

            return View(orders);
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
                    return RedirectToAction("Index", "Eventures");
                }
                Order orderForDb = new Order
                {
                    OrderedOn = DateTime.UtcNow,
                    EventureId = model.EventId,
                    TicketsCount = model.TIcketsCount,
                    CustomerId = userId
                };

                ev.TotalTickets -= model.TIcketsCount;

                context.Events.Update(ev);
                context.Orders.Add(orderForDb);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Eventures");
        }
    }
}
